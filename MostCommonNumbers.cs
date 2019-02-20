using System;
using System.Collections;
using System.Collections.Generic;

namespace LotteryResultsApp
{
    public static class MostCommonNumbers
    {
        // "HowMachNumberOccurs" method finds --top N most common numbers from the lottery results file
        // The algoritm taken from https://www.csharpstar.com/csharp-find-the-most-frequent-element-in-an-array/
        public static List<int> HowMachNumberOccurs(IEnumerable<int> source, int topNValue)
        {
            // Below numbers added to a hash table where key is a number and value its occurence in the file with lottery results
            Hashtable numberOccurence = new Hashtable();

            foreach (int num in source)
            {
                if (!numberOccurence.ContainsKey(num))
                {
                    numberOccurence.Add(num, 1);
                }
                else
                {
                    int tempOccurences = (int)numberOccurence[num];
                    tempOccurences++;
                    numberOccurence.Remove(num);
                    numberOccurence.Add(num, tempOccurences);
                }
            }


            // Adding key values (lottery numbers) from the hash table above which have biggest values (number occurence in the lottery results file)
            List<int> numberOccurenceHashTableValues = new List<int>();
            foreach (int value in numberOccurence.Values)
            {
                numberOccurenceHashTableValues.Add(value);
            }
            // Values sorted and reversed - after that the top N can be taken and returned
            numberOccurenceHashTableValues.Sort();
            numberOccurenceHashTableValues.Reverse();

            List<int> topN = new List<int>(); // here will be written the top N numbers
            // Algoritm which finds corresponding key from the hash table for the the top N numbers (values)
            for (int i = 0; i < topNValue; i++)
            {
                foreach (int key in numberOccurence.Keys)
                {
                    if ((int)numberOccurence[key] == numberOccurenceHashTableValues[i])
                    {
                        topN.Add(key);
                    }
                }

            }

            return topN;
        }
    }
}
