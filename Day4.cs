using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day4
    {
        public static void Part2()
        {
            var input = File.ReadAllLines("DayInputs/day4.txt");
            Dictionary<int, int> cards = new Dictionary<int, int>();
            for(int cardNumberPosition = 0; cardNumberPosition < input.Length; cardNumberPosition++)
            {
                var parts = input[cardNumberPosition].Split(':')[1].Split('|');
                var winningNumbers = parts[0].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                var myNumbers = parts[1].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                var matchingNumbersAmount = winningNumbers.Intersect(myNumbers).Count();
                //Add this card to the cards, or add 1 copy if it already exists
                if (!cards.ContainsKey(cardNumberPosition + 1))
                    cards.Add(cardNumberPosition + 1, 1);
                else
                    cards[cardNumberPosition + 1]++;

                //Add copies for the worth if matchingNumbersAmount > 0
                if(matchingNumbersAmount > 0)
                {
                    for (int n = 0; n < cards[cardNumberPosition + 1]; n++)
                    {
                        for (int i = 1; i < matchingNumbersAmount + 1; i++)
                        {
                            var copyCard = cardNumberPosition + 1 + i;
                            if (!cards.TryGetValue(copyCard, out int card))
                                cards.Add(copyCard, 1);
                            else
                                cards[copyCard]++;
                        }
                    }
                }
            }
            Console.WriteLine("Day 4, part 2: " + cards.Sum(x => x.Value));
        }
        public static void Part1()
        {
            var total  = 0;
            var input = File.ReadAllLines("DayInputs/day4.txt");
            foreach (var line in input)
            {
                var parts = line.Split(':')[1].Split('|');
                var winningNumbers = parts[0].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                var myNumbers = parts[1].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                var worth = 0;
                for(int i = 0; i < winningNumbers.Intersect(myNumbers).Count(); i++)
                {
                    if (worth == 0)
                        worth = 1;
                    else
                        worth *= 2;
                }
                total += worth;
            }
            Console.WriteLine("Day 4, part 1: "+total);
        }
    }
}
