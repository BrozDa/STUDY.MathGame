using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.MathGame
{
    /// <summary>
    /// Represents operations chosen by user within the MathGame
    /// </summary>
    enum Operations
    {
        Addition,
        Substraction,
        Multiplication,
        Division,
        History,
        Exit,
        Menu,
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
        private ConsoleKey userChoice { get; set; }
        private readonly int _maxNumber = 10;

        /// <summary>
        /// Initializes new instance of MathGame class
        /// Initialization of list used to track history
        /// </summary>
        public MathGame()
        {
            history = new List<string>();
        }

        /// <summary>
        /// main method faciliating the game using infinite loop until its broken by user choice
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
                        PlayRound(operation);  
                        break;
                    case Operations.History:
                        DisplayClass.PrintHistory(history);
                        Console.ReadKey();
                        break;
                }
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        private ConsoleKey GetUserInput()
        {
            bool validated = false;

            while (!validated)
            {
                userChoice = Console.ReadKey().Key;
                ConsoleKey validation = Console.ReadKey().Key;
                if(validation == ConsoleKey.Enter)
                {
                    validated = true;
                }
                else
                {
                    Console.WriteLine("\nPlease enter a VALID number representing your choice followed-up by 'Enter': ");
                    Console.Write("Your choice: ");
                }
            }

            return userChoice;
        }
        /// <summary>
        /// 
        /// </summary>
        private void PlayRound(Operations operation)
        {

            int[] numbers = new int[2];
            bool validResponse = true;
            int roundNumber = 1;
            char operationCharacter = GetOperationCharacter(operation);

            while (validResponse)
            {
                if (operation != Operations.Division)
                    numbers = GetOperationNumbers();
                else
                    numbers = GetOperationNumbers(true);

                DisplayClass.PrintGame(roundNumber, numbers, operationCharacter);

                int result = GetResult(numbers, operation);
                int userResult;
                int.TryParse(Console.ReadLine(), out userResult);

                if(userResult == result)
                    roundNumber++;
                else
                {
                    validResponse = false;
                    
                    
                    DisplayClass.PrintEndGame(roundNumber, numbers, operationCharacter,userResult, result);
                    userChoice = Console.ReadKey().Key;
                    AddGameToHistory(operationCharacter, roundNumber);
                    EndRound(userChoice);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        private void EndRound(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
                StartGame();
            else
                Environment.Exit(0);
        }
        /// <summary>
        /// 
        /// </summary>
        private char GetOperationCharacter(Operations operation)
        {
            return operation switch
            {
                Operations.Addition => '+',
                Operations.Substraction => '-',
                Operations.Multiplication => '*',
                Operations.Division => '/',
                _ => throw new NotImplementedException("Invalid ENUM value passed"),
            };
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        private Operations OperationChoice(ConsoleKey userInput)
        {
           switch (userChoice) {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1: 
                    return Operations.Addition;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    return Operations.Substraction;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    return Operations.Multiplication;

                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    return Operations.Division;

                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    return Operations.History;

                case ConsoleKey.Escape: 
                    return Operations.Exit;

                default:
                    return Operations.Menu;

            }
        }
        /// <summary>
        /// 
        /// </summary>
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
                // fisrt getting divisor
                // then, divident will be calculated as random numer times divisor
                int divisor = random.Next(1,_maxNumber);

                numbers[1] = divisor;
                numbers[0] = random.Next(_maxNumber) * divisor;
            }

            return numbers;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddGameToHistory(char operation, int rounds) {
            string game = $"{DateTime.Now.ToString()} | Operation: {operation} | Rounds: {rounds}";
            history.Add(game);
            
        }

    }
}
