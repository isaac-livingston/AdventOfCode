using Challenge2023.Common;
using System.Collections.Frozen;

namespace Challenge2023.Day07
{
    internal abstract class Day07Base : ProblemBase
    {
        public Day07Base() 
        {
            var cards = new Dictionary<string, int>
            {
                ["2"] = 1 << 2,
                ["3"] = 1 << 3,
                ["4"] = 1 << 4,
                ["5"] = 1 << 5,
                ["6"] = 1 << 6,
                ["7"] = 1 << 7,
                ["8"] = 1 << 8,
                ["9"] = 1 << 9,
                ["T"] = 1 << 10,
                ["J"] = 1 << 11,
                ["Q"] = 1 << 12,
                ["K"] = 1 << 13,
                ["A"] = 1 << 14
            };

            Cards = cards.ToFrozenDictionary();
        }

        protected FrozenDictionary<string, int> Cards { get; set; }
    }
}
