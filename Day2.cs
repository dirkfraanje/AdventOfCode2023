using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day2
    {
        public static void Part2()
        {
            int total = 0;
            foreach (var game in System.IO.File.ReadAllLines("DayInputs/day2.txt")){
                total += CubeSetPower(game.Split(':')[1]);
            }
            Console.WriteLine("Day 2, part 2: " + total);
        }

        private static int CubeSetPower(string game)
        {
            var red = 0;
            var blue = 0;
            var green = 0;

            var i = 0;
            var digitBuilder = new StringBuilder();
            var colorBuilder = new StringBuilder();
            while (i < game.Length)
            {
                if (char.IsDigit(game[i]))
                {
                    digitBuilder.Append(game[i]);
                }
                else if (char.IsLetter(game[i]))
                {
                    colorBuilder.Append(game[i]);
                    if (colorBuilder.Length > 2)
                    {
                        switch (colorBuilder.ToString())
                        {
                            case "red":
                                var redCube = int.Parse(digitBuilder.ToString());
                                if (redCube > red)
                                    red = redCube;
                                digitBuilder.Clear();
                                colorBuilder.Clear();
                                break;
                            case "blue":
                                var blueCube = int.Parse(digitBuilder.ToString());
                                if (blueCube > blue)
                                    blue = blueCube;
                                digitBuilder.Clear();
                                colorBuilder.Clear();
                                break;
                            case "green":
                                var greenCube = int.Parse(digitBuilder.ToString());
                                if (greenCube > green)
                                    green = greenCube;
                                digitBuilder.Clear();
                                colorBuilder.Clear();
                                break;
                            default:
                                break;
                        }
                    }
                }
                
                i++;
            }
            return red * blue * green;
        }

        public static void Part1()
        {
            var red = 12;
            var blue = 14;
            var green = 13;
            var total = 0;
            foreach (var game in System.IO.File.ReadAllLines("DayInputs/day2.txt"))
            {
                var parts = game.Split(':');
                if (GamePossible(parts[1], red, blue, green))
                    total += int.Parse(parts[0].Substring(5));
            }
            Console.WriteLine("Day 2, part 1: "+total);
        }

        private static bool GamePossible(string game, int redMax, int blueMax, int greenMax)
        {
            var i = 0;
            var digitBuilder = new StringBuilder();
            var colorBuilder = new StringBuilder();
            while(i < game.Length)
            {
                if (char.IsDigit(game[i]))
                {
                    digitBuilder.Append(game[i]);
                }
                else if (char.IsLetter(game[i]))
                {
                    colorBuilder.Append(game[i]);
                    if (colorBuilder.Length > 2)
                    {
                        switch (colorBuilder.ToString())
                        {
                            case "red":
                                if (int.Parse(digitBuilder.ToString()) > redMax)
                                    return false;
                                digitBuilder.Clear();
                                colorBuilder.Clear();
                                break;
                            case "blue":
                                if (int.Parse(digitBuilder.ToString()) > blueMax)
                                    return false;
                                digitBuilder.Clear();
                                colorBuilder.Clear();
                                break;
                            case "green":
                                if (int.Parse(digitBuilder.ToString()) > greenMax)
                                    return false;
                                digitBuilder.Clear();
                                colorBuilder.Clear();
                                break;
                            default:
                                break;
                        }
                    }
                }
                
                i++;
            }
            return true;
        }
    }
}
