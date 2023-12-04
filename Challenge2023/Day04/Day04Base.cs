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
    }
}
