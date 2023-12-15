using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2023
{
    internal class Day8
    {
        public static void Part1()
        {
            var input = File.ReadAllLines("DayInputs/day8part1.txt");
            var instructions = input[0];
            var elements = input.Skip(2).ToArray();

            var end = "ZZZ";

            var currentElement = elements.First(x=>x.Substring(0,3) == "AAA");
            var i = 0;
            var mod = instructions.Length;
            var steps = 0;
            while (currentElement.Split('=')[0].Trim() != end)
            {
                steps++;
                var instruction = instructions[i];
                var element = "";
                switch (instruction)
                {
                    case 'L':
                        element = currentElement.Split('=')[1].Trim().Substring(1, 3);
                        break;
                    case 'R':
                        element = currentElement.Split('=')[1].Trim().Substring(6,3);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                currentElement = elements.First(x => x.Substring(0, 3) == element);
                i++;
                i = i % mod;
            }
            Console.WriteLine("Day 8, part 1: "+steps);
        }
    }
}
