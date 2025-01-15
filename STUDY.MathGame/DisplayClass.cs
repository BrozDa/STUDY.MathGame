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
        /// Prints header for the menu
        /// </summary>
        public static void PrintMenuHeader()
        {
            Console.Clear();
            int windowWidth = Console.WindowWidth;
            string welcomeMessage = "Welcome to Math Game";
            int leftPadding = (windowWidth - welcomeMessage.Length)/ 2;
                Console.WriteLine(new string('#', windowWidth));
                Console.WriteLine(new string(' ', leftPadding) + "Welcome to Math Game");
                Console.WriteLine(new string('#', windowWidth));
        }
        /// <summary>
        /// Prints header for a the game
        /// </summary>
        /// <param name="roundNumber">Current number of the round</param>
        /// <param name="gameDuration">Duration of current game</param>
        public static void PrintGameHeader(int roundNumber, TimeSpan gameDuration)
        {
            Console.Clear();
            int windowWidth = Console.WindowWidth;
            Console.WriteLine(new string('#', windowWidth));
            Console.WriteLine($"\t" +
                $"Round Duration: 00:00" +
                $"\t\tMath Game round: {roundNumber}" +
                $"\t\tGame Duration: {gameDuration.ToString(@"mm\:ss")}");
            Console.WriteLine(new string('#', windowWidth));
        }
        /// <summary>
        /// Updates menu with current duration for round and the game
        /// </summary>
        /// <param name="roundDuration">Duration of current round</param>
        /// <param name="gameDuration">Duration of current round</param>
        /// <param name="roundNumber">Current number of the round</param>
        public static void UpdateHeaderWithTime(TimeSpan roundDuration, TimeSpan gameDuration, int roundNumber)
        {
            Console.CursorVisible = false;
            int originalTop = Console.CursorTop;
            int originalLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"" +
                $"\tRound Duration: {roundDuration.ToString(@"mm\:ss")}" +
                $"\t\tMath Game round: {roundNumber}" +
                $"\t\tGame Duration: {gameDuration.ToString(@"mm\:ss")}");

            Console.SetCursorPosition(originalLeft, originalTop);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Prints selection menu
        /// </summary>
        public static void PrintMenu()
        {
            PrintMenuHeader();

            Console.WriteLine("Please select one option to start a new game, show game history, or press 'ESC' to exit: ");
            Console.WriteLine();
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Substraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Random game");
            Console.WriteLine();
            Console.WriteLine("6. Select Difficulty");
            Console.WriteLine("7. Show game history");
            Console.WriteLine("8. Exit game");
            Console.WriteLine();
            Console.WriteLine("Please enter a number representing your choice followed-up by 'Enter': ");
            Console.Write("Your choice: ");

        }
        /// <summary>
        /// Dificulty selection menu
        /// </summary>
        /// <param name="current">current difficulty</param>
        public static void PrintDifficultySelectionMenu(Difficulty current)
        {
            PrintMenuHeader();
            int number = 1;
            Console.WriteLine("Select difficulty, current setting is marked by '***':, or hit 'ESC' to exit to main menu ");
            Console.WriteLine();
            foreach (Difficulty dif in Enum.GetValues(typeof(Difficulty)))
            {

                Console.Write(number++ + ": ");

                if(current == dif)
                    Console.Write("***");
                Console.WriteLine(dif);
            }
            Console.WriteLine("4: Exit to main menu");
            Console.WriteLine();
            Console.Write("Your choice: ");
        }
        /// <summary>
        /// Prints equation for current round
        /// </summary>
        /// <param name="roundNumber">number of the round</param>
        /// <param name="numbers">array of numbers used for equation</param>
        /// <param name="operation">opearation characted</param>
        public static void PrintRoundEquation(int roundNumber, int[] numbers, char operation)
        {
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
            PrintMenuHeader();

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
