using Challenge.Common;
using System.Collections.Frozen;

#nullable disable

namespace Challenge2023.Day08
{
    internal abstract class Day08Base : ProblemBase
    {
        protected int[] Steps { get; private set; } = [];

        protected FrozenDictionary<string, string[]> Map { get; private set; }

        protected void LoadData(string[] inputs)
        {
            Steps = inputs.First().Select(x => x.Equals('L') ? 0 : 1).ToArray();
            var coords = inputs.Skip(2).Select(x => x.Replace("(", null).Replace(")", null)).ToArray();
            LoadMap(coords);
        }

        private void LoadMap(string[] coordInputs)
        {
            var coords = new Dictionary<string, string[]>();

            for (int i = 0; i < coordInputs.Length; i++)
            {
                var inputs = coordInputs[i].Split('=', SPLIT_OPTS);
                var key = inputs[0];
                var L0R1 = inputs[1].Split(',', SPLIT_OPTS);

                coords[key] = [L0R1[0], L0R1[1]];
            }

            Map = coords.ToFrozenDictionary();
        }

        protected (long steps, string key) TraverseMap(Func<string, bool> untilDestinationCondition, string key = "AAA", long limit = 1L)
        {
            var iter = 0L;
            var counter = 0L;

            while (untilDestinationCondition(key))
            {
                var lr = Steps[iter];
                key = Map[key][lr];
                iter++;
                counter++;

                if (iter == Steps.Length)
                {
                    iter = 0L;
                }

                if (counter == limit)
                {
                    break;
                }
            }

            return (counter, key);
        }

        /*
            Credit goes to the reddit community for examples and mohammedsouleymane for his C# example 
            that I used to debug my efforts with LCM.  Also, props to mohammedsouleymane for his neat
            incrementing solution.
            
            https://old.reddit.com/r/adventofcode
            https://github.com/mohammedsouleymane/AdventOfCode/blob/main/AdventOfCode/Aoc2023/
         */

        private static long UpdateLCM(long x, long y)
        {
            var max = Math.Max(x, y);
            var min = Math.Min(x, y);

            for (long i = 1; i <= min; i++)
            {
                if (max * i % min == 0)
                {
                    return i * max;
                }
            }

            return min;
        }

        protected long Problem2Doer()
        {
            var keys = Map.Keys.Where(k => k[2] == 'A').ToArray();
            long lcm = 1;

            foreach (var key in keys)
            {
                var opKey = key;
                var count = 0;
                while (opKey[2] != 'Z')
                {
                    opKey = Map[opKey][Steps[count++ % Steps.Length]];
                }

                lcm = UpdateLCM(lcm, count);
            }

            return lcm;
        }
    }
}
