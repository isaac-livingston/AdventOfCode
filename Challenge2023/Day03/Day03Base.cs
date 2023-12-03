using Challenge2023.Common;
using Challenge2023.Day03.Models;

namespace Challenge2023.Day03
{
    internal abstract class Day03Base : ProblemBase
    {
        const string NOT_SYMBOLS = "1234567890.";

        readonly List<Symbol> Symbols = [];

        protected readonly Dictionary<Point, (int partNumber, Symbol anchorSymbol)> SymbolAdjacentDigits = [];

        protected void LoadSybmolsFromMatrix(string[] inputs)
        {
            Symbols.Clear();

            var matrixWidth = inputs[0].Length - 1;
            var matrixHeight = inputs.Length - 1;

            for (var row = 0; row < inputs.Length; row++)
            {
                for (var col = 0; col < inputs[row].Length; col++)
                {
                    var value = inputs[row][col];
                    if (!NOT_SYMBOLS.Contains(value))
                    {
                        var symbol = new Symbol(row, col)
                        {
                            Value = value
                        };

                        SetSymbolInfluence(symbol, matrixHeight, matrixWidth);

                        Symbols.Add(symbol);
                    }
                }
            }
        }

        private static void SetSymbolInfluence(Symbol symbol, int matrixHeight, int matrixWidth)
        {
            var point = symbol.Point;
            var influences = symbol.Influences;

            var onLeft = false;
            var onTop = false;
            var onRight = false;
            var onBottom = false;

            if (point.Row > 0)
            {
                influences.Add(new(point.Row - 1, point.Col));
                onTop = true;
            }

            if (point.Row < matrixHeight)
            {
                influences.Add(new(point.Row + 1, point.Col));
                onBottom = true;
            }

            if (point.Col > 0)
            {
                influences.Add(new(point.Row, point.Col - 1));
                onLeft = true;
            }

            if (point.Col < matrixWidth)
            {
                influences.Add(new(point.Row, point.Col + 1));
                onRight = true;
            }

            if (onLeft && onTop)
            {
                influences.Add(new(point.Row - 1, point.Col - 1));
            }

            if (onLeft && onBottom)
            {
                influences.Add(new(point.Row - 1, point.Col + 1));
            }

            if (onRight && onTop)
            {
                influences.Add(new(point.Row + 1, point.Col - 1));
            }

            if (onRight && onBottom)
            {
                influences.Add(new(point.Row + 1, point.Col + 1));
            }
        }

        protected List<Point> GetPointsOfSymbolAdjacentDigits(string[] inputs)
        {
            var influences = Symbols.SelectMany(x => x.Influences).ToList();
            var digitPoints = new List<Point>();

            foreach (var influence in influences)
            {
                var value = inputs[influence.Row][influence.Col].ToString();
                if (int.TryParse(value, out var digit))
                {
                    digitPoints.Add(influence);
                }
            }

            return digitPoints;
        }

        protected void LoadSymbolAdjacentDigits(string[] inputs, List<Point> symboldAdjacentDigitPoints)
        {
            var maxCol = inputs[0].Length;

            foreach (var point in symboldAdjacentDigitPoints)
            {
                var col = point.Col;

                while (int.TryParse(inputs[point.Row][col].ToString(), out var _) && col > 0)
                {
                    col--;
                }

                if (!int.TryParse(inputs[point.Row][col].ToString(), out var _))
                {
                    col++;
                }

                var startingCol = col;
                var partNumberString = string.Empty;

                while (col < maxCol && int.TryParse(inputs[point.Row][col].ToString(), out var digit))
                {
                    partNumberString += digit.ToString();
                    col++;
                }

                var digitPoint = new Point(point.Row, startingCol);

                var anchorSymbol = Symbols.FirstOrDefault(x => x.Influences.Contains(point))
                                 ?? throw new InvalidOperationException("Could not find anchor symbol");

                SymbolAdjacentDigits[digitPoint] = (int.Parse(partNumberString), anchorSymbol);
            }
        }
    }
}
