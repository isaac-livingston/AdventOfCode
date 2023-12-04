namespace Challenge2023.Day04
{
    internal class Problem2 : Day04Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day04");

            var cards = GetCards(inputs);

            cards = GetExpandedWinners(cards);

            Console.WriteLine();
            Console.WriteLine($"Total: {cards.Count:N0}");
        }
    }
}
