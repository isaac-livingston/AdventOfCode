using Challenge2023.Common;
using System.Collections.Frozen;

namespace Challenge2023.Day07
{
    internal abstract class Day07Base : ProblemBase
    {
        public Day07Base() 
        {
            DefineCards();
        }

        protected abstract void DefineCards();

        protected FrozenDictionary<char, int> Cards { get; set; }

        protected Dictionary<string, (long bet, long scale, long[] power)> Hands { get; private set; } = [];

        protected void LoadHands(string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                var data = inputs[i].Split(' ', SPLIT_OPTS);
                var (scale, power) = MakeHand(data[0]);

                Hands[data[0]] = (long.Parse(data[1]), scale, power);
            }

            Hands = Hands.OrderBy(x => x.Value.scale)
                         .ThenBy(x => x.Value.power[0])
                         .ThenBy(x => x.Value.power[1])
                         .ThenBy(x => x.Value.power[2])
                         .ThenBy(x => x.Value.power[3])
                         .ThenBy(x => x.Value.power[4])
                         .ToDictionary();
        }

        private (long scale, long[] power) MakeHand(string handInput)
        {
            var hand = handInput.ToCharArray();
            var power = new long[5];

            for (int i = 0; i < 5; i++)
            {
                power[i] = Cards[hand[i]];
            }

            return (GetHandScale(handInput), power);
        }

        static int GetHandScale(string handInput)
        {
            var cardCounts = handInput.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            
            if (cardCounts.ContainsValue(5))
            {
                return 6;
            }
            else if(cardCounts.ContainsValue(4))
            {
                return 5;
            }
            else if (cardCounts.ContainsValue(3) && cardCounts.ContainsValue(2))
            {
                return 4;
            }
            else if (cardCounts.ContainsValue(3))
            {
                return 3;
            }
            else if (cardCounts.ContainsValue(2))
            {
                var xx = GroupByCardCount(cardCounts);

                if (xx.ContainsValue(2))
                {
                    return 2;
                }

                return 1;
            }

            return 0;
        }

        static Dictionary<int, int> GroupByCardCount(Dictionary<char, int> cardCounts)
        {
            return cardCounts
                .GroupBy(entry => entry.Value)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(entry => entry.Key).Count()
                );
        }
    }
}
