using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day7
    {
        static List<Tuple<string, int>> FiveOfKinds = new();
        static List<Tuple<string, int>> FourOfKinds = new();
        static List<Tuple<string, int>> FullHouses = new();
        static List<Tuple<string, int>> ThreeOfKinds = new();
        static List<Tuple<string, int>> TwoPairs = new();
        static List<Tuple<string, int>> OnePairs = new();
        static List<Tuple<string, int>> HighCards = new();

       public static void Part1()
        {
            //Rank order, defaultRank (Five of a kind, four of a kind, etc), card


            var input = File.ReadAllLines("DayInputs/day7.txt");
            foreach (var line in input)
            {
                AddToRankPart1(line);
            }
            var all = new List<Tuple<string, int>>();

            var highCards = HighCards.ToArray();
            Array.Sort(highCards, new TupleComparerPart1());
            all.AddRange(highCards);

            var onePairs = OnePairs.ToArray();
            Array.Sort(onePairs, new TupleComparerPart1());
            all.AddRange(onePairs);

            var twoPairs = TwoPairs.ToArray();
            Array.Sort(twoPairs, new TupleComparerPart1());
            all.AddRange(twoPairs);

            var threeOfKinds = ThreeOfKinds.ToArray();
            Array.Sort(threeOfKinds, new TupleComparerPart1());
            all.AddRange(threeOfKinds);

            var fullHouses = FullHouses.ToArray();
            Array.Sort(fullHouses, new TupleComparerPart1());
            all.AddRange(fullHouses);

            var fourOfKinds = FourOfKinds.ToArray();
            Array.Sort(fourOfKinds, new TupleComparerPart1());
            all.AddRange(fourOfKinds);

            var fiveOfKinds = FiveOfKinds.ToArray();
            Array.Sort(fiveOfKinds, new TupleComparerPart1());
            all.AddRange(fiveOfKinds);

            long total = 0;
            for (int i = 0; i < all.Count; i++)
            {
                var card = all[i];
                total += card.Item2 * (i + 1);
            }
            Console.WriteLine("Day 7, part 1: " + total);
            //250773976 to low251058093
        }

        public class TupleComparerPart1 : Comparer<Tuple<string, int>>
        {
            public override int Compare(Tuple<string, int>? x, Tuple<string, int>? y)
            { 
                for (int i = 0; i < x.Item1.Length; i++)
                {
                    switch (x.Item1[i])
                    {
                        case 'A':
                            if (y.Item1[i] != 'A')
                                return 1;
                            else
                                continue;
                        case 'K':
                            if (y.Item1[i] == 'A')
                                return -1;
                            else if (y.Item1[i] != 'K')
                                return 1;
                            else
                                continue;
                        case 'Q':
                            if (y.Item1[i] == 'A' || y.Item1[i] == 'K')
                                return -1;
                            else if (y.Item1[i] != 'Q')
                                return 1;
                            else
                                continue;
                        case 'J':
                            if (y.Item1[i] == 'A' || y.Item1[i] == 'K' || y.Item1[i] == 'Q')
                                return -1;
                            else if (y.Item1[i] != 'J')
                                return 1;
                            else
                                continue;
                        case 'T':
                            if (y.Item1[i] == 'A' || y.Item1[i] == 'K' || y.Item1[i] == 'Q' || y.Item1[i] == 'J')
                                return -1;
                            else if (y.Item1[i] != 'T')
                                return 1;
                            else
                                continue;
                        default:
                            if (!char.IsDigit(y.Item1[i]))
                                return -1;
                            var one = Char.GetNumericValue(x.Item1[i]);
                            var two = Char.GetNumericValue(y.Item1[i]);
                            if (one == two)
                                continue;
                            return one > two ? 1 : -1;
                    }

                }
                throw new Exception();
            }
        }
        private static void AddToRankPart1(string line)
        {
            var lineSplitted = line.Split(' ');
            var lineValue = lineSplitted[0];
            line = lineSplitted[0];
            var bid = int.Parse(lineSplitted[1]);
            if (Regex.Matches(line, $"{line[0]}").Count == 5)
                //Five of a kind
                FiveOfKinds.Add(Tuple.Create(lineValue, bid));
            else if (Regex.Matches(line, $"{line[0]}").Count == 4 || Regex.Matches(line, $"{line[1]}").Count == 4)
                //Four of a kind
                FourOfKinds.Add(Tuple.Create(lineValue, bid));
            else if (Regex.Matches(line, $"{line[0]}").Count == 3 || Regex.Matches(line, $"{line[1]}").Count == 3 || Regex.Matches(line, $"{line[2]}").Count == 3)
            {
                var threeValue = Regex.Matches(line, $"{line[0]}").Count == 3 ? line[0] : Regex.Matches(line, $"{line[1]}").Count == 3 ? line[1] : line[2];
                //Three are equal, check if it's a Full house
                line = line.Remove(line.IndexOf(threeValue), 1);
                line = line.Remove(line.IndexOf(threeValue), 1);
                line = line.Remove(line.IndexOf(threeValue), 1);
                //Full house
                if (Regex.Matches(line, $"{line[0]}").Count == 2)
                    FullHouses.Add(Tuple.Create(lineValue, bid));
                //Three of a kind
                else
                    ThreeOfKinds.Add(Tuple.Create(lineValue, bid));
            }
            else if (line.Any(x => Regex.Matches(line, $"{x}").Count == 2))
            {
                var twoValue = Regex.Matches(line, $"{line[0]}").Count == 2 ? line[0] : Regex.Matches(line, $"{line[1]}").Count == 2 ? line[1] : Regex.Matches(line, $"{line[2]}").Count == 2 ? line[2] : line[3];
                line = line.Remove(line.IndexOf(twoValue), 1);
                line = line.Remove(line.IndexOf(twoValue), 1);

                //Two pair
                if (line.Any(x => Regex.Matches(line, $"{x}").Count == 2))
                    TwoPairs.Add(Tuple.Create(lineValue, bid));
                //One pair
                else
                    OnePairs.Add(Tuple.Create(lineValue, bid));
            }
            //High card
            else
                HighCards.Add(Tuple.Create(lineValue, bid));
        }
    }
}
