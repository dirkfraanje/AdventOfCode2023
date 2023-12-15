using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2023
{
    internal class Day8Part2
    {
        public static void Part2()
        {
            var input = File.ReadAllLines("DayInputs/day8part2.txt");
            var instructions = input[0];
            var elements = input.Skip(2).ToArray();

            var currentElements = elements.Where(x => x[2] == 'A').ToList();
            var loopElements = currentElements.ToArray();
            var i = 0;
            var mod = instructions.Length;
            List<int> stepsTotals = new List<int>();

            foreach (var currentElement in loopElements)
            {
                var steps = 0;
                var checkElement = currentElement;

                while (!(checkElement[2] == 'Z'))
                {
                    steps++;
                    var instruction = instructions[i];
                    var element = "";
                    switch (instruction)
                    {
                        case 'L':
                            element = checkElement.Split('=')[1].Trim().Substring(1, 3);
                            break;
                        case 'R':
                            element = checkElement.Split('=')[1].Trim().Substring(6, 3);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    checkElement = elements.First(x => x.Substring(0, 3) == element);
                    i++;
                    i = i % mod;
                }
                stepsTotals.Add(steps);
            }
            Dictionary<int, int> factors = new();
            //Use Lowest Common Multiple to get the result (Check https://www.cuemath.com/numbers/lcm-least-common-multiple/)
            int a, b;
            foreach (var item in stepsTotals)
            {
                a = item;
                for (b = 2; a > 1; b++)
                    if (a % b == 0)
                    {
                        int x = 0;
                        while (a % b == 0)
                        {
                            a /= b;
                            x++;
                        }
                        if (!factors.ContainsKey(b))
                            factors.Add(b, x);
                        else
                        {
                            if (factors[b] < x)
                                factors[b] = x;
                        }
                    }
            }
            double total = 1;
            foreach (var item in factors)
            {
                var factor = item.Key;
                var power = item.Value;
                total *= Math.Pow(factor, power);
            }
            Console.WriteLine("Day 8, part 1: " + total);
        }

    }

}
