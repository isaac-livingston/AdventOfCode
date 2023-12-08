using Challenge2023.Common;
using System.Collections.Frozen;

namespace Challenge2023.Day07
{
    internal class Problem2 : Day07Base
    {
        protected override void DefineCards()
        {
            var cards = new Dictionary<char, int>
            {
                ['2'] = 1 << 2,
                ['3'] = 1 << 3,
                ['4'] = 1 << 4,
                ['5'] = 1 << 5,
                ['6'] = 1 << 6,
                ['7'] = 1 << 7,
                ['8'] = 1 << 8,
                ['9'] = 1 << 9,
                ['T'] = 1 << 10,
                ['J'] = 1 << 0,
                ['Q'] = 1 << 12,
                ['K'] = 1 << 13,
                ['A'] = 1 << 14
            };

            Cards = cards.ToFrozenDictionary();
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day07");

            stopwatch.Start();

            LoadHands(inputs, jokersWild: true);

            var solution = 0L;
            var iter = 1L;

            foreach (var key in Hands.Keys)
            {
                solution += Hands[key].bet * iter;
                iter++;
            }

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(solution);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
