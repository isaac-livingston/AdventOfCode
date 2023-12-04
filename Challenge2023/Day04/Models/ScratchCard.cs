namespace Challenge2023.Day04.Models
{
    internal class ScratchCard
    {
        public ScratchCard(string data)
        {
            var info = data.Split(':');

            var splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

            CardId = int.Parse(info[0].Split(' ', splitOptions)[1]);

            var numbers = info[1].Split('|', splitOptions)
                                 .Select(x => x.Split(' ', splitOptions)
                                               .Select(x => int.Parse(x)).ToArray())
                                 .ToArray();

            WinningNumbers = numbers[0];
            GivenNumbers = numbers[1];
            MatchedNumbers = WinningNumbers.Intersect(GivenNumbers).ToArray();
            Winnings = (long)Math.Pow(2, MatchedNumbers.Length - 1);
        }

        public int CardId { get; }
        public int[] WinningNumbers { get; }
        public int[] GivenNumbers { get; }
        public int[] MatchedNumbers { get; }
        public long Winnings { get; } 
    }
}
