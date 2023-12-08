using Challenge2023.Common;
using System.Collections.Frozen;
using System.Text;

namespace Challenge2023.Day07
{
    internal class Problem1 : Day07Base
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
                ['J'] = 1 << 11,
                ['Q'] = 1 << 12,
                ['K'] = 1 << 13,
                ['A'] = 1 << 14
            };

            Cards = cards.ToFrozenDictionary();
        }

        public override void RunSolution()
        {
            stopwatch.Start();
            var inputs = GetInputs(folder: "day07");

            //inputs = [
            //    "32T3K 765",
            //    "T55J5 684",
            //    "KK677 28",
            //    "KTJJT 220",
            //    "QQQJA 483",
            //    "QQQAA 483",
            //];

            //inputs = [
            //    "2345A 1",
            //    "Q2KJJ 13",
            //    "Q2Q2Q 19",
            //    "T3T3J 17",
            //    "T3Q33 11",
            //    "2345J 3",
            //    "J345A 2",
            //    "32T3K 5",
            //    "T55J5 29",
            //    "KK677 7",
            //    "KTJJT 34",
            //    "QQQJA 31",
            //    "JJJJJ 37",
            //    "JAAAA 43",
            //    "AAAAJ 59",
            //    "AAAAA 61",
            //    "2AAAA 23",
            //    "2JJJJ 53",
            //    "JJJJ2 41",
            //];

            LoadHands(inputs);
            var sb = new StringBuilder();
            foreach (var key in Hands.Keys)
            {
                var val = $"{key} {Hands[key].bet}:\t{Hands[key].scale}\t{Hands[key].power[0]}\t{Hands[key].power[1]}\t{Hands[key].power[2]}\t{Hands[key].power[3]}\t{Hands[key].power[4]}";
                sb.AppendLine(val);
                Console.WriteLine(val);
            }

            File.WriteAllText("output.csv", sb.ToString());

            var solution = 0L;
            var iter = 1L;

            foreach(var key in Hands.Keys)
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
