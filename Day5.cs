using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day5
    {
        static List<long> seeds = new List<long>();
        static List<List<Tuple<long, long, long>>> currentMaps = new();

        public static void Part2()
        {
            Console.WriteLine("Brute force, may take a while");
            seeds.Clear();
            currentMaps.Clear();
            var input = File.ReadAllLines("DayInputs/day5.txt");
            List<Tuple<long, long>> seedStartAndEnd = new();
            var seedInput = input[0].Split(' ').Skip(1).Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();

            for (int i = 0; i < seedInput.Count; i++)
            {
                var start = seedInput[i];
                i++;
                var end = start + seedInput[i] - 1;
                seedStartAndEnd.Add(Tuple.Create(start, end));
            }
            if (!currentMaps.Any())
            {
                for (int j = 2; j < input.Length; j++)
                {
                    var currentMap = new List<Tuple<long, long, long>>();
                    j++;
                    while (j < input.Length && !string.IsNullOrEmpty(input[j]))
                    {
                        var line = $"{input[j]}".Split(' ');
                        currentMap.Add(Tuple.Create(long.Parse($"{line[0]}"), long.Parse($"{line[1]}"), long.Parse($"{line[2]}")));
                        j++;
                    }
                    currentMaps.Add(currentMap);
                }

            }

            long lowest = Int64.MaxValue;
            foreach (var seedrange in seedStartAndEnd)
            {
                for (long i = seedrange.Item1; i < seedrange.Item2; i++)
                {
                    var result = ChangeSeeds(i);
                    if (result < lowest)
                        lowest = result;

                }
            }

                //189786876

                Console.WriteLine("Day 5, part 2: " + lowest);
        }
        public static void Part1()
        {
            var input = File.ReadAllLines("DayInputs/day5.txt");

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("seeds"))
                {
                    seeds = input[i].Split(' ').Skip(1).Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();
                    i++;
                    continue;
                }
                if (!currentMaps.Any())
                {
                    for (int j = 2; j < input.Length; j++)
                    {
                        var currentMap = new List<Tuple<long, long, long>>();
                        j++;
                        while (j < input.Length && !string.IsNullOrEmpty(input[j]))
                        {
                            var line = $"{input[j]}".Split(' ');
                            currentMap.Add(Tuple.Create(long.Parse($"{line[0]}"), long.Parse($"{line[1]}"), long.Parse($"{line[2]}")));
                            j++;
                        }
                        currentMaps.Add(currentMap);
                    }
                    break;
                }
            }

            long lowest = 0;
            foreach (var seed in seeds)
            {
                var result = ChangeSeeds(seed);
                if (lowest == 0)
                    lowest = result;
                else if (result < lowest)
                    lowest = result;

            }

            Console.WriteLine("Day 5, part 1: " + lowest);
        }

        private static long ChangeSeeds(long seed)
        {
            foreach (var checkmap in currentMaps)
            {

                foreach (var map in checkmap)
                {
                    var sourceRangeStart = map.Item2;
                    var sourceRangeEnd = map.Item2 + map.Item3 - 1;
                    if (seed >= sourceRangeStart && seed <= sourceRangeEnd)
                    {
                        seed = map.Item1 - map.Item2 + seed;
                        break;
                    }

                }
            }
            return seed;
        }
    }
}
