namespace Challenge2023.Day05.Models
{
    internal class Seed(long id)
    {
        public long Id { get; } = id;
        //public long Soil { get; set; }
        //public long Fertilizer { get; set; }
        //public long Water { get; set; }
        //public long Light { get; set; }
        //public long Temperature { get; set; }
        //public long Humidity { get; set; }
        public long Location { get; set; }
    }
}
