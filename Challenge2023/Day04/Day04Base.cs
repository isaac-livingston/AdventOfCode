using Challenge.Common;
using Challenge2023.Day04.Models;

namespace Challenge2023.Day04
{
    internal abstract class Day04Base : ProblemBase
    {
        protected static List<ScratchCard> GetCards(string[] inputs)
        {
            return inputs.Select(x=> new ScratchCard(x)).ToList();
        }

        protected List<ScratchCard> GetExpandedWinners(List<ScratchCard> cards)
        {
            var winners = cards.Where(x => x.MatchedNumbers.Length != 0).ToList();

            var cardTable = cards.ToDictionary(x => x.CardId, x => x);

            foreach (var winner in winners)
            {
                for(var i = 1; i < winner.MatchedNumbers.Length + 1; i++)
                {
                    var cardToAdd = cardTable[winner.CardId + i];

                    cards.Add(cardToAdd);

                    if (cardToAdd.Winnings > 0)
                    {
                        cards.AddRange(ExpandCards(cardToAdd, cardTable, null));
                    }
                }
            }

            return cards;
        }

        private List<ScratchCard> ExpandCards(ScratchCard card, Dictionary<int, ScratchCard> cardTable, List<ScratchCard>? expandedSet)
        {
            expandedSet ??= [];

            for (var i = 1; i < card.MatchedNumbers.Length + 1; i++)
            {
                var cardToAdd = cardTable[card.CardId + i];

                expandedSet.Add(cardToAdd);

                if (cardToAdd.Winnings > 0)
                {
                    ExpandCards(cardToAdd, cardTable, expandedSet);
                }
            }

            return expandedSet;
        }
    }
}
