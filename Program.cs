using System;
using System.IO;
using System.Linq;

namespace LotteryResultsApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string lotteryResultsFilePath = null;
            bool isLotteryResultsFileExists = false;

            while (!isLotteryResultsFileExists) // User will be promted to try again in case of wrong file path provided to the console
            {
                Console.WriteLine("Enter the path to the file with lottery results and press [Enter] button on your keyboard:");
                lotteryResultsFilePath = Console.ReadLine();
                isLotteryResultsFileExists = File.Exists(lotteryResultsFilePath);

                if (!isLotteryResultsFileExists)
                {
                    Console.WriteLine($"Unfortunatly the programm was unable to find a source file with lottery results following the path: \"{lotteryResultsFilePath}\"");
                    Console.WriteLine("Please doble check the path and try again...");
                }
            }

            bool isConsoleCommandCorrect = false; // In case of user entered something wrong to the console he will be prompted to try again
            while (!isConsoleCommandCorrect)
            {
                Console.WriteLine("Please use \"--top N\" command to determine the top N most common winning numbers.");
                Console.WriteLine("The N parameter should be between 1 and 50:");
                string userInput = Console.ReadLine();
                var isTopNumber = int.TryParse(string.Join("", userInput.ToCharArray().Where(Char.IsDigit)), out int topNWinningNumbers);

                if (userInput.Contains("--top " + topNWinningNumbers) && topNWinningNumbers > 0 && topNWinningNumbers <= 50) // Need to check if user's input is in correct format
                {
                    isConsoleCommandCorrect = true;

                    var result = LotteryResultsHandler.FileWithResultsReader(lotteryResultsFilePath);
                    var howMatch = MostCommonNumbers.HowMachNumberOccurs(result, topNWinningNumbers);
                    Console.Clear();

                    Console.WriteLine($"Top {topNWinningNumbers} most common winning numbers printed below:");
                    foreach (int number in howMatch)
                    {
                        Console.WriteLine(number + " ");
                    }
                }
                else
                {
                    Console.WriteLine("You did not provide correct command to the console. Try again...");
                }
            }

            Console.WriteLine("Press any key to close the screen...");
            Console.ReadKey();
        }
    }
}

