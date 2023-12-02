using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day1
    {
        public static void Part1()
        {
            int output = 0;

            foreach (var item in System.IO.File.ReadAllLines("DayInputs/day1part1.txt"))
            {
                char firstValue = '0';
                char secondValue = '0';
                foreach (var position in item)
                {
                    if (char.IsDigit(position))
                    {
                        if (firstValue == '0')
                            firstValue = position;
                        else
                            secondValue = position;
                    }
                    if (secondValue == '0')
                        secondValue = firstValue;
                }
                output += int.Parse($"{firstValue}{secondValue}");
            }

            Console.WriteLine($"Day 1, part 1: {output}");
        }

        public static void Part2()
        {

            int output = 0;
            foreach (var item in System.IO.File.ReadAllLines("DayInputs/day1part2.txt"))
            {
                StringBuilder numbers = new StringBuilder();
                for (int i = 0; i < item.Length; i++)
                {
                    var charValue = item[i];
                    if (char.IsDigit(charValue))
                    {
                        numbers.Append(charValue);
                        continue;
                    }
                    try
                    {
                        if (charValue == 'o' || charValue == 't' || charValue == 'f' || charValue == 's' || charValue == 'e' || charValue == 'n')
                        {

                            switch (charValue)
                            {
                                case 'o':
                                    if (item[i + 1] == 'n' && item[i + 2] == 'e')
                                        numbers.Append('1');
                                    break;
                                case 't':
                                    if (item[i + 1] == 'w' && item[i + 2] == 'o')
                                        numbers.Append('2');
                                    else if (item[i + 1] == 'h' && item[i + 2] == 'r' && item[i + 3] == 'e' && item[i + 4] == 'e')
                                        numbers.Append('3');
                                    break;
                                case 'f':
                                    if (item[i + 1] == 'o' && item[i + 2] == 'u' && item[i + 3] == 'r')
                                        numbers.Append('4');
                                    else if (item[i + 1] == 'i' && item[i + 2] == 'v' && item[i + 3] == 'e')
                                        numbers.Append('5');
                                    break;
                                case 's':
                                    if (item[i + 1] == 'i' && item[i + 2] == 'x')
                                        numbers.Append('6');
                                    else if (item[i + 1] == 'e' && item[i + 2] == 'v' && item[i + 3] == 'e' && item[i + 4] == 'n')
                                        numbers.Append('7');
                                    break;
                                case 'e':
                                    if (item[i + 1] == 'i' && item[i + 2] == 'g' && item[i + 3] == 'h' && item[i + 4] == 't')
                                        numbers.Append('8');
                                    break;
                                case 'n':
                                    if (item[i + 1] == 'i' && item[i + 2] == 'n' && item[i + 3] == 'e')
                                        numbers.Append('9');
                                    break;
                                default:
                                    throw new ArgumentException("Should not get here: " + charValue);
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }

                }
                char firstValue = '0';
                char secondValue = '0';
                foreach (var position in numbers.ToString())
                {
                    if (char.IsDigit(position))
                    {
                        if (firstValue == '0')
                            firstValue = position;
                        else
                            secondValue = position;
                    }
                }
                if (secondValue == '0')
                    secondValue = firstValue;
                output += int.Parse($"{firstValue}{secondValue}");
            }
            Console.WriteLine($"Day 1, part 2: {output}");
        }
    }
}
