using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day9Part2
    {
        public static void Part2()
        {
            var input = File.ReadAllLines("DayInputs/day9.txt");
            var total = 0;
            foreach (var line in input)
            {
                var startValues = line.Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).Reverse().ToList();
                var toAdd = new List<int>();
                var values = getNextValues(startValues, toAdd);
                var nextvalue = toAdd.Sum();
                total += nextvalue;
            }
            Console.WriteLine("Day 9, part 2: "+total);
        }

        private static List<int> getNextValues(List<int> startValues, List<int> toAdd)
        {
            var nextValues = new List<int>();
            toAdd.Add(startValues.Last());
            for (int i = startValues.Count - 1; i > 0; i--)
            {
                var value1 = startValues[i];
                var value2 = startValues[i - 1];
                nextValues.Insert(0, value1 - value2);
            }
            if (nextValues.All(x => x == 0))
                return nextValues;
            
            return getNextValues(nextValues, toAdd);
        }
    }
}
