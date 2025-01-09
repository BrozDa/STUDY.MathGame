using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.MathGame
{
    internal static class DisplayClass
    {
        public static void PrintHeader(int roundNumber = 0)
        {
            Console.Clear();

            if (roundNumber == 0) {
                Console.WriteLine(new string('#', 88));
                Console.WriteLine(new string(' ', 34) + "Welcome to Math Game");
                Console.WriteLine(new string('#', 88));
            }
            else
            {
                Console.WriteLine(new string('#', 88));
                Console.WriteLine(new string(' ', 34) + "Math Game round " + roundNumber);
                Console.WriteLine(new string('#', 88));
            }
        }
        public static void PrintMenu()
        {
            PrintHeader();

            Console.WriteLine("Please select one option to start a new game, show game history, or press 'ESC' to exit: ");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Substraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine();
            Console.WriteLine("5. Show game history");
            Console.WriteLine();
            Console.Write("Your choice: ");

        }
        public static void PrintGame(int roundNumber, int[] numbers, char operation)
        {
            
            PrintHeader(roundNumber);
            
            Console.Write($"{numbers[0]} {operation} {numbers[1]} = ");
        }

        public static void PrintEndGame(int roundNumber, int[] numbers, char operation, int userInput, int result)
        {
            PrintHeader(roundNumber);
            Console.WriteLine($"{numbers[0]} {operation} {numbers[1]} = {userInput}");
            Console.WriteLine("You loose");
            Console.WriteLine($"Your answer: {userInput}, correct answer: {result}");
            Console.WriteLine();
            Console.WriteLine("Press 'Enter' to return to the main menu or press anything else to exit");

        }
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
