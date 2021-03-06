﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LotteryResultsApp
{
    public static class LotteryResultsHandler
    {
        public static double processingProgressInPerCents = 0;
        public static IEnumerable<int> FileWithResultsReader(string lotteryResultsFilePath)
        {
            // Variables below needed for output lottery results file processing progress
            int percentCounter = 0;
            double fileSize = new FileInfo(lotteryResultsFilePath).Length;
            double processedLinesSize = 262; // First line size is 262 bytes

            foreach (var line in File.ReadLines(lotteryResultsFilePath).Skip(1))
            {
                var tempLine = line.Split('\t');

                // First two fields are date and district_number so the will be skipped from processing of winning numbers below
                yield return Convert.ToInt32(tempLine[2]);
                yield return Convert.ToInt32(tempLine[3]);
                yield return Convert.ToInt32(tempLine[4]);
                yield return Convert.ToInt32(tempLine[5]);
                yield return Convert.ToInt32(tempLine[6]);
                yield return Convert.ToInt32(tempLine[7]);
                yield return Convert.ToInt32(tempLine[8]);
                yield return Convert.ToInt32(tempLine[9]);
                yield return Convert.ToInt32(tempLine[10]);
                yield return Convert.ToInt32(tempLine[11]);

                processedLinesSize = processedLinesSize + line.Length;

                // Calculation and printing out to the console current progress
                processingProgressInPerCents = (processedLinesSize / fileSize) * 100;
                if ((int)processingProgressInPerCents == percentCounter && percentCounter <= 100)
                {
                    Progress();
                    percentCounter++;
                }
            }
        }

        // Method which prints file processing progress to the console
        public static void Progress()
        {
            Console.Clear();
            if ((int)processingProgressInPerCents <= 100)
            {
                Console.WriteLine("Current lottery results file processing progress is: " + (int)processingProgressInPerCents + "%");
            }
        }

    }
}
