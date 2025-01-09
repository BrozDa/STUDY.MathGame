using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.MathGame
{
    enum Operations: int
    {
        Addition = 1,
        Substraction =2,
        Multiplication = 3,
        Division = 4,
        History = 5
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
            DisplayClass.PrintMenu();
            userChoice = Console.ReadKey().Key;
            Console.ReadKey();
            while (true) {
                OperationChoice(userChoice);
            }
            
        }

        private void OperationChoice(ConsoleKey userInput)
        {
           switch (userChoice) {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1: 
                    Addition(); 
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2: 
                    Substraction(); 
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3: 
                    Multiplication(); 
                    break;

                case ConsoleKey.NumPad4:
                case ConsoleKey.D4: 
                    Division(); 
                    break;

                case ConsoleKey.NumPad5:
                case ConsoleKey.D5: 
                    ShowGameHistory(); break;

                case ConsoleKey.Escape: Environment.Exit(0); break;

                default: StartGame();break;

            }
        }
        private void Addition() 
        {
            bool userAnswer = true;
            int roundNumber = 1;
            Random random = new Random();
            int firstNumber = 0;
            int secondNumber = 0;
            int userInput= 0;
            int operationResult = 0;

            while (userAnswer == true) {
                firstNumber = random.Next(_maxNumber + 1);
                secondNumber = random.Next(_maxNumber + 1);
                operationResult = firstNumber + secondNumber;

                DisplayClass.PrintGame(roundNumber, firstNumber, secondNumber, '+');
                
                int.TryParse(Console.ReadLine(), out userInput);

                if (operationResult == userInput)
                    roundNumber++;
                else
                    userAnswer = false;
            }
            DisplayClass.PrintEndGame(roundNumber,firstNumber,secondNumber,'+', userInput, operationResult);

            userChoice = Console.ReadKey().Key;
            if (userChoice == ConsoleKey.Enter)
            {
                AddGameToHistory('+', roundNumber);
                DisplayClass.PrintMenu();
            }
            else
            {
                Environment.Exit(0);
            }
        
        }
        
        private void Substraction() { }
        private void Multiplication() { }
        private void Division() { }

        private void AddGameToHistory(char operation, int rounds) {
            string game = $"{DateTime.Now.ToString()} | Operation: {operation} | Rounds: {rounds}";
            history.Add(game);
            
        } 
        private void ShowGameHistory()
        {
            DisplayClass.PrintHeader();

            DisplayClass.PrintHistory(history);
            userChoice = Console.ReadKey().Key;
            if (userChoice == ConsoleKey.Enter)
            {
                DisplayClass.PrintMenu();
            }
            else
            {
                Environment.Exit(0);
            }
        }

    }
}
