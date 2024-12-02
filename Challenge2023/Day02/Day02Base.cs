using Challenge.Common;
using Challenge2023.Day02.Models;

namespace Challenge2023.Day02
{
    internal abstract class Day02Base : ProblemBase
    {
        protected readonly List<Game> Games = [];

        static int GetGameId(string input)
        {
            var gameIdString = input.Split(':')[0]
                                    .Split(' ')[1];

            return int.Parse(gameIdString);
        }

        static GameDataSet GetGameDataSet(string setData)
        {
            var results = setData.Split(',');

            var gameDataSet = new GameDataSet();

            foreach (var result in results)
            {
                var values = result.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var count = int.Parse(values[0]);

                switch (values[1])
                {
                    case "red":
                        gameDataSet.Red = count;
                        break;
                    case "green":
                        gameDataSet.Green = count;
                        break;
                    case "blue":
                        gameDataSet.Blue = count;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return gameDataSet;
        }

        static Game LoadGameData(Game game, string input)
        {
            var gameData = input.Split(':')[1];
            var gameSets = gameData.Split(';');

            foreach (var gameSet in gameSets)
            {
                var gameDataSet = GetGameDataSet(gameSet);
                game.Results.Add(gameDataSet);
            }

            return game;
        }

        protected void LoadPlayedGames(string[] gameRecords)
        {
            foreach (var gameRecord in gameRecords)
            {
                var game = new Game
                {
                    Id = GetGameId(gameRecord)
                };

                LoadGameData(game, gameRecord);

                Games.Add(game);
            }
        }
    }
}
