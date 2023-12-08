using Challenge2023.Common;
using System.Collections.Frozen;

#nullable disable

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

        protected Dictionary<string, (long bet, long rank, long[] power)> Hands { get; private set; } = [];

        protected void LoadHands(string[] inputs, bool jokersWild = false)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                var data = inputs[i].Split(' ', SPLIT_OPTS);
                var (rank, power) = MakeHand(data[0], jokersWild);

                Hands[data[0]] = (long.Parse(data[1]), rank, power);
            }

            Hands = Hands.OrderBy(x => x.Value.rank)
                         .ThenBy(x => x.Value.power[0])
                         .ThenBy(x => x.Value.power[1])
                         .ThenBy(x => x.Value.power[2])
                         .ThenBy(x => x.Value.power[3])
                         .ThenBy(x => x.Value.power[4])
                         .ToDictionary();
        }

        private (long rank, long[] power) MakeHand(string handInput, bool jokersWild = false)
        {
            var hand = handInput.ToCharArray();
            var power = new long[hand.Length];

            for (int i = 0; i < 5; i++)
            {
                power[i] = Cards[hand[i]];
            }

            return (GetHandScale(handInput, jokersWild), power);
        }

        int GetHandScale(string handInput, bool jokersWild = false)
        {
            if (jokersWild)
            {
                var noJoke = handInput.Replace("J", null);

                if (noJoke.Length.Equals(0))
                {
                    handInput = string.Empty.PadLeft(5, 'A');
                }
                else
                {
                    var bestCard = GetCardCounts(noJoke).Select(x => (x.Key, x.Value, Cards[x.Key]))
                                                        .OrderByDescending(x => x.Value)
                                                        .ThenByDescending(x => x.Item3)
                                                        .FirstOrDefault().Key;

                    handInput = noJoke.PadLeft(5, bestCard);
                }
            }

            var cardCounts = GetCardCounts(handInput);

            if (cardCounts.ContainsValue(5))
            {
                return 6;
            }
            else if (cardCounts.ContainsValue(4))
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

        static Dictionary<char, int> GetCardCounts(string handInput) =>
            handInput.GroupBy(c => c)
                     .ToDictionary(g => g.Key, g => g.Count());

        static Dictionary<int, int> GroupByCardCount(Dictionary<char, int> cardCounts) =>
            cardCounts.GroupBy(entry => entry.Value)
                      .ToDictionary(group => group.Key, group => group.Select(entry => entry.Key).Count());
    }
}
