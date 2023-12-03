using Challenge2023.Day02.Models;

namespace Challenge2023.Day02
{
    internal class Problem2 : Day02Base
    {
        public static int GetGamePower(Game game)
        {
            var powerSet = new GameDataSet
            {
                Red = Math.Max(1, game.Results.Max(x => x.Red)),
                Green = Math.Max(1, game.Results.Max(x => x.Green)),
                Blue = Math.Max(1, game.Results.Max(x => x.Blue))
            };

            return powerSet.Red * powerSet.Blue * powerSet.Green;
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day02");

            LoadPlayedGames(inputs);

            List<int> gamePowers = [];

            foreach (var game in Games)
            {
                var gamePower = GetGamePower(game);
                gamePowers.Add(gamePower);
            }

            Console.WriteLine($"Total: {gamePowers.Sum():N0}");
        }
    }
}
