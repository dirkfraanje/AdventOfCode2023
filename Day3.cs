using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day3
    {
        public static void Part2()
        {

            var input = File.ReadAllLines("DayInputs/day3.txt");
            var firstLine = input[0];
            char[,] matrix = new char[firstLine.Length, input.Length];
            var total = 0;

            //Build up the matrix
            for (int x = 0; x < input.Length; x++)
            {
                var line = input[x];
                for (int y = 0; y < line.Length; y++)
                    matrix[x, y] = line[y];
            }

            //Check the matrix
            for (int x = 0; x < input.Length; x++)
            {
                var line = input[x];
                var digitBuilder = new StringBuilder();
                var yPositionCheck = new List<int>();
                for (int y = 0; y < line.Length; y++)
                {
                    if (matrix[x, y] == '*')
                    {
                        var checkAndGear = CheckAdjacentToStar(matrix, x, y);
                        if (checkAndGear[0] == 2)
                        {
                            Console.WriteLine(checkAndGear[1]);
                            Console.WriteLine(checkAndGear[2]);
                            Console.WriteLine();
                            total += (checkAndGear[1] * checkAndGear[2]);
                        }
                    }

                }
            }
            Console.WriteLine("Day 3, part 2: " + total);
        }

        private static int[] CheckAdjacentToStar(char[,] matrix, int x, int y)
        {

            var totalTrue = 0;
            var gearRatio1 = 0;
            var gearRatio2 = 0;
            var skipNext = false;
            //Check row above
            if (AdjacentIsDigit(matrix, x - 1, y - 1))
            {
                totalTrue++;
                skipNext = true;
                gearRatio1 = GetValue(matrix, x - 1, y - 1);
            }

            if (AdjacentIsDigit(matrix, x - 1, y))
            {
                if (!skipNext)
                {
                    totalTrue++;
                    skipNext = true;
                    if (gearRatio1 == 0)
                        gearRatio1 = GetValue(matrix, x - 1, y);
                    else
                        gearRatio2 = GetValue(matrix, x - 1, y);
                }

            }
            else
                skipNext = false;

            if (!skipNext)
            {
                if (AdjacentIsDigit(matrix, x - 1, y + 1))
                {
                    totalTrue++;
                    if (gearRatio1 == 0)
                        gearRatio1 = GetValue(matrix, x - 1, y + 1);
                    else
                        gearRatio2 = GetValue(matrix, x - 1, y + 1);
                }

            }


            //Check left and right
            if (AdjacentIsDigit(matrix, x, y - 1))
            {
                totalTrue++;
                if (gearRatio1 == 0)
                    gearRatio1 = GetValue(matrix, x, y - 1);
                else
                    gearRatio2 = GetValue(matrix, x, y - 1);
            }

            if (AdjacentIsDigit(matrix, x, y + 1))
            {
                totalTrue++;
                if (gearRatio1 == 0)
                    gearRatio1 = GetValue(matrix, x, y + 1);
                else
                    gearRatio2 = GetValue(matrix, x, y + 1);
            }

            skipNext = false;
            //Check row below
            if (AdjacentIsDigit(matrix, x + 1, y - 1))
            {
                totalTrue++;
                skipNext = true;
                if (gearRatio1 == 0)
                    gearRatio1 = GetValue(matrix, x + 1, y - 1);
                else
                    gearRatio2 = GetValue(matrix, x + 1, y - 1);
            }
            if (AdjacentIsDigit(matrix, x + 1, y))
            {
                if (!skipNext)
                {
                    totalTrue++;
                    skipNext = true;
                    if (gearRatio1 == 0)
                        gearRatio1 = GetValue(matrix, x + 1, y);
                    else
                        gearRatio2 = GetValue(matrix, x+1, y);
                }
            }
            else
                skipNext = false;
            if (!skipNext)
            {
                if (AdjacentIsDigit(matrix, x + 1, y + 1))
                {
                    totalTrue++;
                    if (gearRatio1 == 0)
                        gearRatio1 = GetValue(matrix, x + 1, y + 1);
                    else
                        gearRatio2 = GetValue(matrix, x+ 1, y + 1);
                }

            }


            return new[] { totalTrue, gearRatio1, gearRatio2 };
        }

        private static int GetValue(char[,] matrix, int x, int y)
        {
            var stringValue = new StringBuilder();

            if (char.IsDigit(matrix[x, y - 1]))
            {
                if (char.IsDigit(matrix[x, y - 2]))
                    stringValue.Append(matrix[x, y - 2]);
                stringValue.Append(matrix[x, y - 1]);
            }
            stringValue.Append(matrix[x, y]);
            if (char.IsDigit(matrix[x, y + 1]))
                stringValue.Append(matrix[x, y + 1]);
            else
                return int.Parse(stringValue.ToString());
            if (char.IsDigit(matrix[x, y + 2]))
                stringValue.Append(matrix[x, y + 2]);
            return int.Parse(stringValue.ToString());
        }


        private static bool AdjacentIsDigit(char[,] matrix, int x, int y)
        {
            try
            {
                return char.IsDigit(matrix[x, y]);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Part1()
        {

            var input = File.ReadAllLines("DayInputs/day3.txt");
            var firstLine = input[0];
            char[,] matrix = new char[firstLine.Length, input.Length];
            var total = 0;

            //Build up the matrix
            for (int x = 0; x < input.Length; x++)
            {
                var line = input[x];
                for (int y = 0; y < line.Length; y++)
                    matrix[x, y] = line[y];
            }

            //Check the matrix
            for (int x = 0; x < input.Length; x++)
            {
                var line = input[x];
                var digitBuilder = new StringBuilder();
                var yPositionCheck = new List<int>();
                for (int y = 0; y < line.Length; y++)
                {
                    if (char.IsDigit(matrix[x, y]))
                    {
                        digitBuilder.Append(matrix[x, y]);
                        yPositionCheck.Add(y);
                    }

                    else if (!char.IsDigit(matrix[x, y]) && digitBuilder.Length > 0)
                    {
                        if (CheckAdjacentToSymbol(matrix, yPositionCheck, x))
                            total += int.Parse(digitBuilder.ToString());
                        yPositionCheck.Clear();
                        digitBuilder.Clear();
                    }

                }
                if (digitBuilder.Length > 0)
                {
                    if (CheckAdjacentToSymbol(matrix, yPositionCheck, x))
                        total += int.Parse(digitBuilder.ToString());
                    yPositionCheck.Clear();
                    digitBuilder.Clear();
                }
            }
            Console.WriteLine("Day 3, part 1: " + total);
        }

        private static bool CheckAdjacentToSymbol(char[,] matrix, List<int> yPositionsCheck, int x)
        {
            foreach (int y in yPositionsCheck)
            {

                //Check row above
                if (AdjacentIsSymbol(matrix, x - 1, y - 1))
                    return true;
                if (AdjacentIsSymbol(matrix, x - 1, y))
                    return true;
                if (AdjacentIsSymbol(matrix, x - 1, y + 1))
                    return true;

                //Check left and right
                if (AdjacentIsSymbol(matrix, x, y - 1))
                    return true;
                if (AdjacentIsSymbol(matrix, x, y + 1))
                    return true;

                //Check row below
                if (AdjacentIsSymbol(matrix, x + 1, y - 1))
                    return true;
                if (AdjacentIsSymbol(matrix, x + 1, y))
                    return true;
                if (AdjacentIsSymbol(matrix, x + 1, y + 1))
                    return true;
            }
            return false;
        }

        private static bool AdjacentIsSymbol(char[,] matrix, int x, int y)
        {
            try
            {
                return !char.IsDigit(matrix[x, y]) && matrix[x, y] != '.';
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
