using Challenge2023.Common;
using Challenge2023.Day07.Comparers;
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

            Hands = Hands.OrderBy(x => x.Value, new MyVerySpecificDay7TupleComparer())
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

            return (GetHandRank(handInput, jokersWild), power);
        }

        int GetHandRank(string handInput, bool jokersWild = false)
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

            if (cardCounts.ContainsValue(5)) // five of a kind
            {
                return 6;
            }
            else if (cardCounts.ContainsValue(4)) // four of a kind
            {
                return 5;
            }
            else if (cardCounts.ContainsValue(3) && cardCounts.ContainsValue(2)) // full house
            {
                return 4;
            }
            else if (cardCounts.ContainsValue(3)) // three of a kind
            {
                return 3;
            }
            else if (cardCounts.ContainsValue(2)) // at least one pair
            {
                //check if we have two pairs
                var countGroups = GroupByCardCount(cardCounts);
                if (countGroups.ContainsValue(2))
                {
                    return 2;
                }

                return 1;
            }

            return 0;
        }

        /// <summary>
        /// group by the card and count how many groups there are
        /// </summary>
        static Dictionary<char, int> GetCardCounts(string handInput) =>
            handInput.GroupBy(c => c)
                     .ToDictionary(g => g.Key, g => g.Count());

        /// <summary>
        /// group by the card count and determin how many cards exist per count
        /// </summary>
        static Dictionary<int, int> GroupByCardCount(Dictionary<char, int> cardCounts) =>
            cardCounts.GroupBy(n => n.Value)
                      .ToDictionary(g => g.Key, g => g.Select(x => x.Key).Count());
    }
}
