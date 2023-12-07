namespace Challenge2023.Day05.Models
{
    internal class Seed(long id)
    {
        public long Id { get; } = id;
        public long Location { get; set; }
    }
}
