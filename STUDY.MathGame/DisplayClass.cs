using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.MathGame
{
    /// <summary>
    /// Static class supporting all menu outputs 
    /// </summary>
    internal static class DisplayClass
    {
        /// <summary>
        /// Prints header for each output
        /// </summary>
        /// <param name="roundNumber">Optional parameter, if not specified then header for completely new game is printed</param>
        public static void PrintHeader(int roundNumber = 0)
        {
            Console.Clear();
            int windowWidth = Console.WindowWidth;
            string welcomeMessage = "Welcome to Math Game";
            int leftPadding = (windowWidth - welcomeMessage.Length)/ 2;

            if (roundNumber == 0) {
                Console.WriteLine(new string('#', windowWidth));
                Console.WriteLine(new string(' ', leftPadding) + "Welcome to Math Game");
                Console.WriteLine(new string('#', windowWidth));
            }
            else
            {
                Console.WriteLine(new string('#', windowWidth));
                Console.WriteLine(new string(' ', leftPadding) + "Math Game round " + roundNumber);
                Console.WriteLine(new string('#', windowWidth));
            }
        }
        /// <summary>
        /// Prints selection menu
        /// </summary>
        public static void PrintMenu()
        {
            PrintHeader();

            Console.WriteLine("Please select one option to start a new game, show game history, or press 'ESC' to exit: ");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Substraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Random game");
            Console.WriteLine();
            Console.WriteLine("6. Show game history");
            Console.WriteLine("7. Exit game");
            Console.WriteLine();
            Console.WriteLine("Please enter a number representing your choice followed-up by 'Enter': ");
            Console.Write("Your choice: ");

        }
        /// <summary>
        /// Prints equation for current round
        /// </summary>
        /// <param name="roundNumber">number of the round</param>
        /// <param name="numbers">array of numbers used for equation</param>
        /// <param name="operation">opearation characted</param>
        public static void PrintRound(int roundNumber, int[] numbers, char operation)
        {
            
            PrintHeader(roundNumber);
            
            Console.Write($"{numbers[0]} {operation} {numbers[1]} = ");
        }
        /// <summary>
        /// Prints summary and correct result for lost round    
        /// </summary>
        // <param name="roundNumber">number of the round</param>
        /// <param name="numbers">array of numbers used for equation</param>
        /// <param name="operation">opearation characted</param>
        /// <param name="userInput">Result entered by the user</param>
        /// <param name="result">Actual result of the equation</param>
        public static void PrintEndGame(int roundNumber, int[] numbers, char operation, int userInput, int result)
        {
            
            Console.WriteLine("You loose");
            Console.WriteLine($"Your answer: {userInput}, correct answer: {result}");
            Console.WriteLine();
            Console.WriteLine("Press 'Enter' to return to the main menu or press anything else to exit");

        }
        /// <summary>
        /// Prints list of played games in current session
        /// </summary>
        /// <param name="history">List containing game history</param>
        public static void PrintHistory(List<string> history) {
            Console.Clear();
            PrintHeader();

            if (history.Count == 0)
            {
                Console.WriteLine("No games in history");
            }
            else {
                int index = 1;
                foreach (string line in history) {
                    Console.WriteLine($"{index}: {line}");
                    index++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press 'Enter' to return to the main menu or press anything else to exit");
        }


    }
}
