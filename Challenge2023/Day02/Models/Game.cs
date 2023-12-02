namespace Challenge2023.Day02.Models
{
    internal class Game
    {
        public int Id { get; set; }
        public List<GameDataSet> Results { get; set; } = [];
    }
}
