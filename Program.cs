using System;
using System.IO;
using System.Linq;

namespace LotteryResultsApp
{
    class Program
    {
        // Method which derermines that --top N command is in correct format and numbers are in the correct range
        public static bool IsTopNInTheCorrectFormat(string topN)
        {
            var isTopNumber = int.TryParse(string.Join("", topN.ToCharArray().Where(char.IsDigit)), out int topNWinningNumbers);
            if (topN.Contains("--top " + topNWinningNumbers) && topNWinningNumbers > 0 && topNWinningNumbers <= 50)
            {
                return true;
            }
                return false;
        }
        static void Main(string[] args)
        {
            /*
             Currently the program takes parameters from the command lines arguments.
             If you wish to manually input parameters to the console -  comment out the code under the first region block and uncomment the second one, build and run the program.
            */
            #region
            int.TryParse(string.Join("", args[1].ToCharArray().Where(Char.IsDigit)), out int topNWinningNumbers);
            var lotteryResultsFilePath = args[0];
            if (File.Exists(lotteryResultsFilePath) && IsTopNInTheCorrectFormat(args[1])) // Still missing check that lottery results file is in correct format
            {
                var result = LotteryResultsHandler.FileWithResultsReader(lotteryResultsFilePath); // Calling the method which reads LotteryResults file line by line and displays processing progress
                var howMatch = MostCommonNumbers.HowMachNumberOccurs(result, topNWinningNumbers); // Calling the method which calculates occurance of each winning number and returns --top N most common numbers
                Console.Clear();

                Console.WriteLine($"Top {topNWinningNumbers} most common winning numbers printed below:");
                foreach (int number in howMatch)
                {
                    Console.WriteLine(number + " ");
                }
            }
            else if (!IsTopNInTheCorrectFormat(args[1]))
            {
                Console.WriteLine($"Now the \"--top N\" parameter is \"{args[1]}\". The N parameter should be between 1 and 50 and written in the following format \"--top N\". Fix it, restart the program and try again...");
            }
            else
            {
                Console.WriteLine($"Unfortunatly the programm was unable to find a source file with lottery results following the path: \"{lotteryResultsFilePath}\"");
                Console.WriteLine("Please double check the path, restart the program and try again...");
            }
            #endregion

            #region
            //string lotteryResultsFilePath = null;
            //bool isLotteryResultsFileExists = false;

            //while (!isLotteryResultsFileExists) // User will be promted to try again in case of wrong file path provided to the console
            //{
            //    Console.WriteLine("Enter the path to the file with lottery results and press [Enter] button on your keyboard:");
            //    lotteryResultsFilePath = Console.ReadLine();
            //    isLotteryResultsFileExists = File.Exists(lotteryResultsFilePath);

            //    if (!isLotteryResultsFileExists)
            //    {
            //        Console.WriteLine($"Unfortunatly the programm was unable to find a source file with lottery results following the path: \"{lotteryResultsFilePath}\"");
            //        Console.WriteLine("Please double check the path and try again...");
            //    }
            //}

            //bool isConsoleCommandCorrect = false; // In case of user entered something wrong to the console he will be prompted to try again
            //while (!isConsoleCommandCorrect)
            //{
            //    Console.WriteLine("Please use \"--top N\" command to determine the top N most common winning numbers.");
            //    Console.WriteLine("The N parameter should be between 1 and 50:");
            //    string userInput = Console.ReadLine();
            //    var isTopNumber = int.TryParse(string.Join("", userInput.ToCharArray().Where(Char.IsDigit)), out int topNWinningNumbers);

            //    if (userInput.Contains("--top " + topNWinningNumbers) && topNWinningNumbers > 0 && topNWinningNumbers <= 50) // Need to check if user's input is in correct format
            //    {
            //        isConsoleCommandCorrect = true;

            //        var result = LotteryResultsHandler.FileWithResultsReader(lotteryResultsFilePath); // Calling the method which reads LotteryResults file line by line and displays processing progress
            //        var howMatch = MostCommonNumbers.HowMachNumberOccurs(result, topNWinningNumbers); // Calling the method which calculates occurance of each winning number and returns --top N most common numbers
            //        Console.Clear();

            //        Console.WriteLine($"Top {topNWinningNumbers} most common winning numbers printed below:");
            //        foreach (int number in howMatch)
            //        {
            //            Console.WriteLine(number + " ");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("You did not provide correct command to the console. Try again...");
            //    }
            //}
            #endregion

            Console.WriteLine("Press any key to close the screen...");
            Console.ReadKey();
        }
    }
}

