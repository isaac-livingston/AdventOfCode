namespace Challenge2023.Day04
{
    internal class Problem1 : Day04Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day04");

            var cards = GetCards(inputs);

            Console.WriteLine();
            Console.WriteLine($"Total: {cards.Sum(x => x.Winnings):N0}");
        }
    }
}
