using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace STUDY.MathGame
{
    /// <summary>
    /// Represents operations chosen by user within the MathGame
    /// </summary>
    enum Operations
    {
        Addition = 1,
        Substraction =2,
        Multiplication = 3,
        Division = 4,
        Random = 5,
        History = 6,
        Exit = 7,
    }
    /// <summary>
    /// Class supporting the Math game
    /// </summary>
    internal class MathGame
    {
        /// <summary>
        /// list property is used to track history games
        /// userChoice key property is used to track user input
        /// _maxNumber readonly field is used to determine max number for math operations
        /// </summary>
        private List<string> history { get; set; }
        private int userChoice { get; set; }
        private int _maxNumber = 10;
        private int _round = 0;
        private Stopwatch _roundTime;
        private Stopwatch _gameTime;
        private System.Timers.Timer _timer;


        /// <summary>
        /// Initializes new instance of MathGame class
        /// Initialization of list used to track history
        /// </summary>
        public MathGame()
        {
            history = new List<string>();
            _roundTime = new Stopwatch();
            _gameTime = new Stopwatch();
            _timer = new System.Timers.Timer();
        }

        public void StartTimer()
        {
            _roundTime.Start();
            _timer.Enabled = true;
            _timer.Interval = 1000;
            _timer.AutoReset = true;
            _timer.Elapsed += new ElapsedEventHandler(delegate { DisplayClass.UpdateHeaderWithTime(_roundTime.Elapsed, _gameTime.Elapsed, _round);});
        }
        public void StopTimer()
        {
            _roundTime.Reset();
            _timer.Enabled= false;
        }
        /// <summary>
        /// Faciliates the game using infinite loop until its broken by user choice
        /// </summary>
        public void StartGame()
        {   
            while (true) {

                DisplayClass.PrintMenu();
                userChoice = GetUserInput();

                Operations operation = OperationChoice(userChoice);
                int[] numbers = new int[2];

                switch (operation) {
                    case Operations.Exit:
                        Environment.Exit(0);
                        break;
                    case Operations.Addition:
                    case Operations.Substraction:
                    case Operations.Multiplication:
                    case Operations.Division:
                    case Operations.Random:
                        PlayGame(operation);
                        break;
                    case Operations.History:
                        DisplayClass.PrintHistory(history);
                        Console.ReadKey();
                        break;
                }
            }
            
        }
        /// <summary>
        /// Gets user's input
        /// </summary>
        /// <returns>Numeric value representation of pressed key</returns>
        private int GetUserInput()
        {
            bool validated = false;

            string? input;
            int numericInput = 0;

            while (!validated) {
                input = Console.ReadLine();
                if (!int.TryParse(input, out numericInput))
                {
                    Console.WriteLine("\nPlease enter a NUMBER representing your choice followed-up by 'Enter': ");
                    Console.Write("Your choice: ");
                }
                else if (numericInput < 1 || numericInput > 7)
                {
                    Console.WriteLine("\nPlease enter a VALID number representing your choice followed-up by 'Enter': ");
                    Console.Write("Your choice: ");
                }
                else
                {
                    validated = true;
                }

            }
            return numericInput;
        }
        /// <summary>
        /// Faciliating a single round of the game
        /// </summary>
        /// <param name="operation">Operation which user choose</param>
        private void PlayGame(Operations operation)
        {

            _gameTime.Start();

            bool validResponse = true;
            _round = 1;
            char operationCharacter = GetOperationCharacter(operation);

            while (validResponse)
            {
                validResponse = PlayRound(operation);
                _round++;
            }

            
            _round = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>True, if user's answer is corrent, false otherwise</returns>
        private bool PlayRound(Operations operation)
        {
            StartTimer();
            _timer.Elapsed += new ElapsedEventHandler(delegate { DisplayClass.UpdateHeaderWithTime(_roundTime.Elapsed, _gameTime.Elapsed, _round);});
            DisplayClass.PrintGameHeader(_round, _gameTime.Elapsed);

            Operations roundStart = operation;
            int[] numbers = new int[2];

            if (operation == Operations.Random)
                operation = GetRandomOperation();

            if (operation != Operations.Division)
                numbers = GetOperationNumbers();
            else
                numbers = GetOperationNumbers(true);

            char operationCharacter = GetOperationCharacter(operation);
            DisplayClass.PrintRoundEquation(_round, numbers, operationCharacter);

            int result = GetResult(numbers, operation);
            int userResult;
            int.TryParse(Console.ReadLine(), out userResult);

            StopTimer();

            if (userResult == result)
            {
                return true;
            }
            else {
                operationCharacter = GetOperationCharacter(roundStart);
                EndCurrentGame(_round, numbers, operationCharacter, userResult, result, _gameTime.Elapsed);
                _gameTime.Reset();
                return false;
            }



        }
        private Operations GetRandomOperation()
        {
            Random random = new Random();
            return (random.Next(1, 5)) switch {
                1 => Operations.Addition,
                2 => Operations.Substraction,
                3 => Operations.Multiplication,
                4 => Operations.Division,
                _ => throw new NotImplementedException("Invalid ENUM passed during getting random value")
            }; 
        }

        /// <summary>
        /// Handles end of the round, adding game to history and user choice to continue or exit the app
        /// </summary>
        /// <param name="roundNumber">Number of current round played</param>
        /// <param name="numbers">Numbers used for the equation</param>
        /// <param name="operationCharacter">Character representing the operation</param>
        /// <param name="userResult">user input - invalid result to the equation</param>
        /// <param name="result">Valid result of the equation</param>
        private void EndCurrentGame(int roundNumber, int[] numbers, char operationCharacter, int userResult, int result, TimeSpan gameTime)
        {
            DisplayClass.PrintEndGame(roundNumber, numbers, operationCharacter, userResult, result);
            ConsoleKey input = Console.ReadKey().Key;
            AddGameToHistory(operationCharacter, roundNumber,gameTime);
            if (input != ConsoleKey.Enter)
                Environment.Exit(0);
               
        }
        /// <summary>
        /// Receive appropriate character for each operation, used for logging games to history
        /// </summary>
        /// <param name="operation">Operation user choose for the game</param>
        /// <returns>Character representing each operation</returns>
        private char GetOperationCharacter(Operations operation)
        {
            return operation switch
            {
                Operations.Addition => '+',
                Operations.Substraction => '-',
                Operations.Multiplication => '*',
                Operations.Division => '/',
                Operations.Random => '@',
                _ => throw new NotImplementedException("Invalid ENUM value passed for getting a character"),
            };
        }
        /// <summary>
        /// Calculates result of the current round
        /// </summary>
        /// <param name="numbers">Numbers generated for current round</param>
        /// <param name="operation">Operation user choose for the game</param>
        /// <returns>integer representing the result</returns>
        private int GetResult(int[] numbers, Operations operation)
        {
            return operation switch
            {
                Operations.Addition => numbers[0] + numbers[1],
                Operations.Substraction => numbers[0] - numbers[1],
                Operations.Multiplication => numbers[0] * numbers[1],
                Operations.Division => numbers[0] / numbers[1],
                _ => throw new NotImplementedException("Invalid ENUM value passed")
            };
        }
        /// <summary>
        /// Translate user input to specific operations represented by Enum
        /// </summary>
        /// <param name="numericInput">Numberic value of key pressed by the user</param>
        /// <returns>Enum value representing chosen operation</returns>
        private Operations OperationChoice(int numericInput)
        {
            return numericInput switch
            {
                1 => Operations.Addition,
                2 => Operations.Substraction,
                3 => Operations.Multiplication,
                4 => Operations.Division,
                5 => Operations.Random,
                6 => Operations.History,
                7 => Operations.Exit,
                _ => throw new NotImplementedException("Invalid ENUM passed")
            };
        }
        /// <summary>
        /// MGet numbers used for the calculations within the game
        /// </summary>
        /// <param name="division">Optional parameter to represent division operation</param>
        /// <returns>int array of 2 values representing generated numbers</returns>
        private int[] GetOperationNumbers(bool division = false)
        {
            Random random = new Random();
            int[] numbers = new int[2];
            if (!division)
            {
                numbers[0] = random.Next(_maxNumber);
                numbers[1] = random.Next(_maxNumber);
            }
            else
            {
                // division operands Divident/ Divisor

                int divident = random.Next(_maxNumber);
                int[] divisors = FindAllDivisors(divident);

                numbers[0] = divident;
                numbers[1] = divisors[random.Next(0, divisors.Length)];
            }

            return numbers;
        }

        /// <summary>
        /// Add played game to the history list
        /// </summary>
        /// <param name="operation">Represents operand using in the game</param>
        /// <param name="rounds">Represents number of rounds played</param>
        private void AddGameToHistory(char operation, int rounds, TimeSpan gameTime) {
            string game = $"{DateTime.Now.ToString()} | Operation: {operation} | Rounds: {rounds} | Time {gameTime.Seconds}s";
            history.Add(game);
            
        }
        /// <summary>
        /// Receive all divisors of a number which returns an integer
        /// </summary>
        /// <param name="number">Divident used for the operation</param>
        /// <returns>Array containing all divisors</returns>
        private int[] FindAllDivisors(int number)
        {
            int squareRoot = (int)Math.Pow(number, 0.5);
            List<int> dividents = new List<int>();
            dividents.Add(1);
            for (int i = 2; i <= squareRoot; i++) {
                if (number % i == 0) {
                    dividents.Add(i);
                    dividents.Add(number / i);

                }
            }
            // dont need to be sorted
            return dividents.ToArray();
        }

    }
}
