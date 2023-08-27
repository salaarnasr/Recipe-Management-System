using System;

namespace RecipeManagementSystem.Utils
{
    public static class ConsoleUtils
    {
        public static void DisplayHeader(string title)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine(title);
            Console.WriteLine("=========================================");
        }

        public static int GetChoice(int min, int max)
        {
            int choice;
            while (true)
            {
                Console.Write($"Please enter a number between {min} and {max}: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }

        public static int GetPositiveInt(string prompt)
        {
            int number;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out number) && number > 0)
                {
                    return number;
                }
                Console.WriteLine("Please enter a positive number.");
            }
        }

    }
}



