using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode2023.Day7;

namespace AdventOfCode2023
{
    internal class Day7Part2
    {
        static List<Tuple<string, int>> FiveOfKinds = new();
        static List<Tuple<string, int>> FourOfKinds = new();
        static List<Tuple<string, int>> FullHouses = new();
        static List<Tuple<string, int>> ThreeOfKinds = new();
        static List<Tuple<string, int>> TwoPairs = new();
        static List<Tuple<string, int>> OnePairs = new();
        static List<Tuple<string, int>> HighCards = new();

        public static void Part2()
        {
            //Rank order, defaultRank (Five of a kind, four of a kind, etc), card


            var input = File.ReadAllLines("DayInputs/day7.txt");
            foreach (var line in input)
            {
                AddToRankPart2(line);
            }
            var all = new List<Tuple<string, int>>();

            var highCards = HighCards.ToArray();
            Array.Sort(highCards, new TupleComparerPart2());
            all.AddRange(highCards);

            var onePairs = OnePairs.ToArray();
            Array.Sort(onePairs, new TupleComparerPart2());
            all.AddRange(onePairs);

            var twoPairs = TwoPairs.ToArray();
            Array.Sort(twoPairs, new TupleComparerPart2());
            all.AddRange(twoPairs);

            var threeOfKinds = ThreeOfKinds.ToArray();
            Array.Sort(threeOfKinds, new TupleComparerPart2());
            all.AddRange(threeOfKinds);

            var fullHouses = FullHouses.ToArray();
            Array.Sort(fullHouses, new TupleComparerPart2());
            all.AddRange(fullHouses);

            var fourOfKinds = FourOfKinds.ToArray();
            Array.Sort(fourOfKinds, new TupleComparerPart2());
            all.AddRange(fourOfKinds);

            var fiveOfKinds = FiveOfKinds.ToArray();
            Array.Sort(fiveOfKinds, new TupleComparerPart2());
            all.AddRange(fiveOfKinds);

            long total = 0;
            for (int i = 0; i < all.Count; i++)
            {
                var card = all[i];
                total += card.Item2 * (i + 1);
            }
            Console.WriteLine("Day 7, part 2: " + total);
            //257689946 is to too high  249841284 to high 261517256 to high wrong 249923123 246392050 249521841
        }
        public class TupleComparerPart2 : Comparer<Tuple<string, int>>
        {
            public override int Compare(Tuple<string, int>? x, Tuple<string, int>? y)
            {
                if (x.Item1 == y.Item1)
                    return 0;
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
                            if (y.Item1[i] == 'J')
                                continue;
                            else
                                return -1;
                        case 'T':
                            if (y.Item1[i] == 'A' || y.Item1[i] == 'K' || y.Item1[i] == 'Q')
                                return -1;
                            else if (y.Item1[i] != 'T')
                                return 1;
                            else
                                continue;
                        default:
                            if (y.Item1[i] == 'J')
                                return 1;
                            if (!char.IsDigit(y.Item1[i]))
                                return -1;
                            var one = Char.GetNumericValue(x.Item1[i]);
                            var two = Char.GetNumericValue(y.Item1[i]);
                            if (one == two)
                                continue;
                            return one > two ? 1 : -1;
                    }

                }
                return 0;
            }
        }

        private static void AddToRankPart2(string line)
        {
            var lineSplitted = line.Split(' ');
            var lineValue = lineSplitted[0];
            var bid = int.Parse(lineSplitted[1]);
            CardsEnum rankResult = GetRankOrder(lineValue);
            switch (rankResult)
            {
                case CardsEnum.FiveOfKind:
                    FiveOfKinds.Add(Tuple.Create(lineValue, bid));
                    break;
                case CardsEnum.FourOfKind:
                    FourOfKinds.Add(Tuple.Create(lineValue, bid));
                    break;
                case CardsEnum.FullHouse:
                    FullHouses.Add(Tuple.Create(lineValue, bid));
                    break;
                case CardsEnum.ThreeOfKind:
                    ThreeOfKinds.Add(Tuple.Create(lineValue, bid));
                    break;
                case CardsEnum.TwoPair:
                    TwoPairs.Add(Tuple.Create(lineValue, bid));
                    break;
                case CardsEnum.OnePair:
                    OnePairs.Add(Tuple.Create(lineValue, bid));
                    break;
                case CardsEnum.HighCard:
                    HighCards.Add(Tuple.Create(lineValue, bid));
                    break;
            }
            //if (lineValue.Contains('J'))
               // Console.WriteLine(lineValue + " " + rankResult);
        }

        private static CardsEnum GetRankOrder(string lineValue)
        {
            if (IsFiveOfKind(lineValue))
                return CardsEnum.FiveOfKind;
            if (IsFourOfKind(lineValue))
                return CardsEnum.FourOfKind;
            if (IsFullHouse(lineValue))
                return CardsEnum.FullHouse;
            if (IsThreeOfKind(lineValue))
                return CardsEnum.ThreeOfKind;
            if (IsTwoPair(lineValue))
                return CardsEnum.TwoPair;
            if (IsOnePair(lineValue))
                return CardsEnum.OnePair;

            //throw new NotImplementedException();
            return CardsEnum.HighCard;
        }

        private static bool IsOnePair(string line)
        {
            if (line.Any(x => Regex.Matches(line, $"{x}").Count == 2))
            {
                return true;
            }
            if (!line.Contains('J'))
                return false;
            if (Regex.Matches(line, "J").Count() == 1)
            {
                return true;
            }
            throw new ArgumentException();
        }

        private static bool IsTwoPair(string line)
        {
            var debugValue = line;
            if (line.Any(x => Regex.Matches(line, $"{x}").Count == 2))
            {
                var twoValue = Regex.Matches(line, $"{line[0]}").Count == 2 ? line[0] : Regex.Matches(line, $"{line[1]}").Count == 2 ? line[1] : Regex.Matches(line, $"{line[2]}").Count == 2 ? line[2] : line[3];
                line = line.Remove(line.IndexOf(twoValue), 1);
                line = line.Remove(line.IndexOf(twoValue), 1);

                //Two pair
                if (line.Any(x => Regex.Matches(line, $"{x}").Count == 2))
                    return true;
                else if (!line.Contains('J'))
                    return false;
                else
                    throw new NotImplementedException();
            }
            else
            {
                //Console.WriteLine(debugValue);
                return false;
            }
        }

        private static bool IsThreeOfKind(string line)
        {
            var debugValue = line;
            if (Regex.Matches(line, $"{line[0]}").Count == 3 || Regex.Matches(line, $"{line[1]}").Count == 3 || Regex.Matches(line, $"{line[2]}").Count == 3)
            {
                return true;
            }
            else if (!line.Contains('J'))
                return false;
            var jCount = Regex.Matches(line, "J").Count;
            for (int i = 0; i < jCount; i++)
            {
                line = line.Remove(line.IndexOf('J'), 1);
            }
            if (line.Count() == 4)
            {
                if (line[0] == line[1] || line[0] == line[2] || line[0] == line[3] ||
                    line[1] == line[2] || line[1] == line[3] ||
                    line[2] == line[3])
                    return true;
                else return false;
            }
            if (line.Count() == 3)
            {
                return true;
            }
            throw new Exception();
        }

        private static bool IsFullHouse(string lineValue)
        {
            var debugValue = lineValue;
            if (Regex.Matches(lineValue, $"{lineValue[0]}").Count == 3 || Regex.Matches(lineValue, $"{lineValue[1]}").Count == 3 || Regex.Matches(lineValue, $"{lineValue[2]}").Count == 3)
            {
                var threeValue = Regex.Matches(lineValue, $"{lineValue[0]}").Count == 3 ? lineValue[0] : Regex.Matches(lineValue, $"{lineValue[1]}").Count == 3 ? lineValue[1] : lineValue[2];
                //Three are equal, check if it's a Full house
                lineValue = lineValue.Remove(lineValue.IndexOf(threeValue), 1);
                lineValue = lineValue.Remove(lineValue.IndexOf(threeValue), 1);
                lineValue = lineValue.Remove(lineValue.IndexOf(threeValue), 1);
                //Full house
                if (Regex.Matches(lineValue, $"{lineValue[0]}").Count == 2)
                    return true;
                
            }
            if (!lineValue.Contains('J'))
                return false;
            var jCount = Regex.Matches(lineValue, "J").Count;
            if(jCount == 1)
            {
                foreach (var charVal in lineValue)
                {
                    var newVal = lineValue.Replace(charVal, 'J');
                    if (Regex.Matches(newVal, $"{newVal[0]}").Count == 3 || Regex.Matches(newVal, $"{newVal[1]}").Count == 3 || Regex.Matches(newVal, $"{newVal[2]}").Count == 3)
                    {
                        var threeValue = Regex.Matches(newVal, $"{newVal[0]}").Count == 3 ? newVal[0] : Regex.Matches(newVal, $"{newVal[1]}").Count == 3 ? newVal[1] : newVal[2];
                        //Three are equal, check if it's a Full house
                        newVal = newVal.Remove(newVal.IndexOf(threeValue), 1);
                        newVal = newVal.Remove(newVal.IndexOf(threeValue), 1);
                        newVal = newVal.Remove(newVal.IndexOf(threeValue), 1);
                        //Full house
                        if (Regex.Matches(newVal, $"{newVal[0]}").Count == 2)
                            return true;

                    }

                }
            }
            return false;
        }

        private static bool IsFourOfKind(string lineValue)
        {
            var debugValue = lineValue;
            if (Regex.Matches(lineValue, $"{lineValue[0]}").Count == 4 || Regex.Matches(lineValue, $"{lineValue[1]}").Count == 4)
                return true;
            if (!lineValue.Contains('J'))
                return false;
            var jCount = Regex.Matches(lineValue, "J").Count;
            for (int i = 0; i < jCount; i++)
            {
                lineValue = lineValue.Remove(lineValue.IndexOf('J'), 1);
            }
            //If only 2 values are left, there were 3 J's so it's Four of a kind
            if (lineValue.Count() == 2)
                return true;
            if (lineValue.Count() == 3)
            {
                if (lineValue[0] == lineValue[1] || lineValue[0] == lineValue[2] || lineValue[1] == lineValue[2])
                    return true;
                else return false;
            }
            if (lineValue.Count() == 4)
            {
                if (Regex.Matches(lineValue, $"{lineValue[0]}").Count == 3 || Regex.Matches(lineValue, $"{lineValue[1]}").Count == 3)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private static bool IsFiveOfKind(string lineValue)
        {
            if (Regex.Matches(lineValue, $"{lineValue[0]}").Count == 5)
                return true;
            if (!lineValue.Contains('J'))
                return false;
            var jCount = Regex.Matches(lineValue, "J").Count;
            for (int i = 0; i < jCount; i++)
            {
                lineValue = lineValue.Remove(lineValue.IndexOf('J'), 1);
            }
            //If only 1 value is left it's five of Kind
            if (lineValue.Count() == 1)
                return true;
            if (lineValue.Count() == 2)
            {
                if (lineValue[0] == lineValue[1])
                    return true;
                else return false;
            }
            if (lineValue.Count() == 3)
            {
                if (lineValue[0] == lineValue[1] && lineValue[0] == lineValue[2])
                    return true;
                else return false;
            }
            if (lineValue.Count() == 4)
            {
                if (lineValue[0] == lineValue[1] && lineValue[0] == lineValue[2] && lineValue[0] == lineValue[3])
                    return true;
                else return false;
            }
            return false;
        }
    }
}
