using System;
using System.IO;
using Mono.Options;

namespace LotteryResultsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // These variables will be set when the command line is parsed
            int topNWinningNumbers = 0;
            bool isTopNWinningNumbersInteger = false;
            string lotteryResultsFilePath = "";
            bool showHelp = false;

            // These are the available options
            var options = new OptionSet();
            options.Add("path=", "Lottery results file path.",
              v => lotteryResultsFilePath = v);
            options.Add("top=", "Top N winning numbers to be returned.",
              v => isTopNWinningNumbersInteger = int.TryParse(v, out topNWinningNumbers));
            options.Add("h|help", "Show this message and exit.",
              v => showHelp = v != null);

            // In case of options can not be parsed exeption message will be shown
            try
            {
                options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("LotteryResultsApp: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `LotteryResultsApp --help' for more information.");
                return;
            }
            // If user wants to see the help - it will be shown for him
            if (showHelp)
            {
                ShowHelp(options);
                return;
            }

            if (args.Length >= 2 && File.Exists(lotteryResultsFilePath) && topNWinningNumbers > 0 && topNWinningNumbers <= 50) // If everything is correct - file processing starts
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
            else if (args.Length < 2) // This block is executed when user missed some argument/arguments
            {
                Console.WriteLine("You did not provide correct ammount of arguments. Try `LotteryResultsApp --help' for more information.");
            }
            else if (!File.Exists(lotteryResultsFilePath)) // This block is executed when user provided wrong file path
            {
                Console.WriteLine($"Unfortunately the program did not find the file following the path {lotteryResultsFilePath}. Please double check the path.");
                Console.WriteLine("Try `LotteryResultsApp --help' for more information.");
                Console.WriteLine("Restart the program and try again...");
            }
            else // This block is executed when user provided out of range winning number
            {
                Console.WriteLine("The N parameter should be between 1 and 50. Try `LotteryResultsApp --help' for more information.");
            }

            Console.WriteLine("Press any button on your keyboard to exit the application...");
            Console.ReadKey();
        }

        // Method which displays command line options help
        static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: The program determines the top N most common winning numbers. The path to lottery results file and top N winning numbers should be provided as command line arguments. E.g.:");
            Console.WriteLine(@">C:\AppFolder\LotteryResultsApp.exe --path=C:\WinningNumbers\LotteryResults.txt --top=5");
            Console.WriteLine("As a result of the program execution (after pressing Enter button) you will see the top N most common winning numbers in the console output.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}

