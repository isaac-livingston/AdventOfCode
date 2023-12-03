using Challenge2023.Day02.Models;

namespace Challenge2023.Day02
{
    internal class Problem1 : Day02Base
    {
        public static int TestGame(GameDataSet testSet, Game game)
        {
            var results = game.Results;

            return true switch
            {
                true when results.Any(x => x.Red > testSet.Red) => 0,
                true when results.Any(x => x.Green > testSet.Green) => 0,
                true when results.Any(x => x.Blue > testSet.Blue) => 0,
                _ => game.Id
            };
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day02");

            LoadPlayedGames(inputs);

            var testGameDataSet = new GameDataSet
            {
                Red = 12,
                Green = 13,
                Blue = 14
            };

            List<int> validGameIds = [];

            foreach(var game in Games)
            {
                var testResult = TestGame(testGameDataSet, game);
                validGameIds.Add(testResult);
            }

            Console.WriteLine($"Total: {validGameIds.Sum():N0}");
        }
    }
}
