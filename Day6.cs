using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day6
    {
        public static void Part2()
        {
            var input = File.ReadAllLines("DayInputs/day6.txt");
            var raceLength = long.Parse(String.Concat(input[0].Split(':')[1].Where(c => !Char.IsWhiteSpace(c))));
            var currentRecord = long.Parse(String.Concat(input[1].Split(':')[1].Where(c => !Char.IsWhiteSpace(c))));
            var total = 1;

            
            var distanceBeaten = 0;
            
            for (int j = 1; j < raceLength + 1; j++)
            {
                var distanceTravelled = j * (raceLength - j);
                if (distanceTravelled > currentRecord)
                    distanceBeaten++;
            }
            total *= distanceBeaten;

            Console.WriteLine("Day 6, part 2: " + total);
        }
        public static void Part1()
        {
            var input = File.ReadAllLines("DayInputs/day6.txt");
            var times = input[0].Split(':')[1].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
            var distances = input[1].Split(':')[1].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
            var total = 1;
            for (int i = 0; i < times.Count; i++)
            {
                var raceLength = times[i];
                var distanceBeaten = 0;
                var currentRecord = distances[i];
                for (int j = 1; j < raceLength + 1; j++)
                {
                    var distanceTravelled = j * (raceLength - j);
                    if (distanceTravelled > currentRecord)
                        distanceBeaten++;
                }
                total *= distanceBeaten;
            }
            Console.WriteLine("Day 6, part 1: "+total);
        }
    }
}
