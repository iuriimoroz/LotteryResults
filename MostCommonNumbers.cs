using System.Collections.Generic;
using System.Linq;

namespace LotteryResultsApp
{
    public static class MostCommonNumbers
    {
        // "HowMachNumberOccurs" method finds --top N most common numbers from the lottery results file
        // The algoritm taken from https://www.csharpstar.com/csharp-find-the-most-frequent-element-in-an-array/
        public static List<int> HowMachNumberOccurs(IEnumerable<int> source, int topNValue)
        {
            // Below numbers added to a dictionary where key is a number and value its occurence value in the file with lottery results
            Dictionary<int, int> numberOccurence = new Dictionary<int, int>();

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

            // Sorting dictionary values by most common occurence of the key
            var sortedDictionary = from entry in numberOccurence orderby entry.Value descending select entry;

            // Top N numbers will be added to the List<T> below and returned as a result of the method
            List<int> topN = new List<int>();
            foreach (var item in sortedDictionary.Take(topNValue))
            {
                topN.Add(item.Key);
            }

            return topN;
        }
    }
}
