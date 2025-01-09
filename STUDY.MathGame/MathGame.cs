using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.MathGame
{
    enum Operations
    {
        Addition = 1,
        Substraction = 2,
        Multiplication = 3,
        Division = 4,
        History,
        Exit,
        Menu,
    }
   
    internal class MathGame
    {
        private List<string> history;
        private ConsoleKey userChoice;
        private readonly int _maxNumber = 10;

        public MathGame()
        {
            history = new List<string>();
        }


        public void StartGame()
        {
            

            while (true) {

                DisplayClass.PrintMenu();
                userChoice = Console.ReadKey().Key;
                Console.ReadKey();
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
        public void PlayRound(Operations operation)
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
        private void EndRound(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
                StartGame();
            else
                Environment.Exit(0);
        }
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
        
    
        private void AddGameToHistory(char operation, int rounds) {
            string game = $"{DateTime.Now.ToString()} | Operation: {operation} | Rounds: {rounds}";
            history.Add(game);
            
        }

    }
}
