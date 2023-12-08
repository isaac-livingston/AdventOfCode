namespace Challenge2023.Day07.Comparers
{
    internal class MyVerySpecificDay7TupleComparer : IComparer<(long bet, long rank, long[] power)>
    {
        public int Compare((long bet, long rank, long[] power) x, (long bet, long rank, long[] power) y)
        {
            int result = x.rank.CompareTo(y.rank);

            if (result != 0)
            {
                return result;
            }

            for (int i = 0; i < Math.Min(x.power.Length, y.power.Length); i++)
            {
                result = x.power[i].CompareTo(y.power[i]);

                if (result != 0)
                {
                    return result;
                }
            }

            return x.power.Length.CompareTo(y.power.Length);
        }
    }
}
