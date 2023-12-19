namespace Challenge2023.Day13.Models
{
    internal class Pattern(string[] input)
    {
        public int VerticalOffset { get; private set; } = 0;

        public string[] HorizontalLines { get; } = GetHorizontalLines(input);

        public string[] VerticalLines { get; } = GetVerticalLines(input);

        private static string[] GetHorizontalLines(string[] input)
        {
            var offset = 0;
            return input.Skip(offset).ToArray();
        }

        private static string[] GetVerticalLines(string[] input)
        {
            var offset = 0;
            var vLines = new List<string>();

            for (var c = offset; c < input[0].Length; c++)
            {
                string line = string.Empty;
                for (var r = 0; r < input.Length; r++)
                {
                    line += input[r][c];
                    
                }
                vLines.Add(line);
            }

            return [.. vLines];
        }
    }
}
