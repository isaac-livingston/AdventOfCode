using Challenge2023.Day02.Models;
using Interfaces;
using System.Text;

namespace Challenge2023.Day02
{
    internal class Problem2 : IProblem
    {
        private const string DAY_FOLDER = "day02";

        readonly List<Game> Games = [];

        static string[] GetInputs()
        {
            var records = File.ReadAllLines(@$"{DAY_FOLDER}\input.txt", Encoding.UTF8);

            for (int i = 0; i < records.Length; i++)
            {
                records[i] = records[i].ToLower();
            }

            return records;
        }

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

        void LoadPlayedGames(string[] gameRecords)
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

        public void RunSolution(params object[] vars)
        {
            var inputs = GetInputs();

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
