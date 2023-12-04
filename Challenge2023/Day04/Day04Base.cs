using Challenge2023.Common;
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

            var expandedSet = new List<ScratchCard>();

            foreach (var winner in winners)
            {
                for(var i = 1; i < winner.MatchedNumbers.Length + 1; i++)
                {
                    var cardToAdd = cards.First(x => x.CardId == winner.CardId + i);

                    expandedSet.Add(cardToAdd);

                    if (cardToAdd.Winnings > 0)
                    {
                        expandedSet.AddRange(ExpandCards(cardToAdd, cards, null));
                    }
                }
            }

            cards.AddRange(expandedSet);

            return cards;
        }

        private List<ScratchCard> ExpandCards(ScratchCard card, List<ScratchCard> cards, List<ScratchCard>? expandedSet)
        {
            expandedSet ??= [];

            for (var i = 1; i < card.MatchedNumbers.Length + 1; i++)
            {
                var cardToAdd = cards.First(x => x.CardId == card.CardId + i);

                expandedSet.Add(cardToAdd);

                if (cardToAdd.Winnings > 0)
                {
                    ExpandCards(cardToAdd, cards, expandedSet);
                }
            }

            return expandedSet;
        }
    }
}
