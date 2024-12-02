using Challenge2023.Day23.Models;
using System.Drawing;
using System.Runtime.Versioning;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Challenge2023.Day23.Visualizer
{
    [SupportedOSPlatform("windows")]
    internal class TilePlotter(int plotH, int plotW, List<Tile> tiles, Guid pathFinderId)
    {
        public void CreatePathBitmap(string path, string fileName)
        {
            var bitmap = new Bitmap(plotW, plotH, PixelFormat.Format32bppPArgb);

            using var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //graphics.Clear(Color.White);
            graphics.Clear(Color.Transparent);


            var pen = new Pen(Color.Black, 1);

            foreach (var tile in tiles)
            {
                var x = tile.Column * 10;
                var y = tile.Row * 10;

                if (tile.StartTile)
                {
                    graphics.FillRectangle(Brushes.Green, x, y, 10, 10);
                }
                else if (tile.EndTile)
                {
                    graphics.FillRectangle(Brushes.Red, x, y, 10, 10);
                }
                else if (tile.PathFinders.Any(p => p.Id == pathFinderId))
                {
                    graphics.FillRectangle(Brushes.Blue, x, y, 10, 10);
                }
                else if (tile.PossibleMoves == Enums.Moves.N)
                {
                    //graphics.FillRectangle(Brushes.Black, x, y, 10, 10);
                    //graphics.FillRectangle(Brushes.Transparent, x, y, 10, 10);
                }
                else
                {
                    
                    graphics.FillRectangle(Brushes.Gray, x, y, 10, 10);
                    graphics.FillRectangle(Brushes.White, x + 1, y + 1, 8, 8);
                }

                if (tile.PossibleMoves == Enums.Moves.U)
                {
                    graphics.DrawString("^", new Font("Consolas", 8), Brushes.Black, x, y);
                }
                else if (tile.PossibleMoves == Enums.Moves.D)
                {
                    graphics.DrawString("V", new Font("Consolas", 8), Brushes.Black, x, y);
                }
                else if (tile.PossibleMoves == Enums.Moves.L)
                {
                    graphics.DrawString("<", new Font("Consolas", 8), Brushes.Black, x, y);
                }
                else if (tile.PossibleMoves == Enums.Moves.R)
                {
                    graphics.DrawString(">", new Font("Consolas", 8), Brushes.Black, x, y);
                }

                if (tile.PossibleMoves.HasFlag(Enums.Moves.U))
                {
                    graphics.DrawLine(pen, x + 5, y, x + 5, y - 5);
                }

                if (tile.PossibleMoves.HasFlag(Enums.Moves.D))
                {
                    graphics.DrawLine(pen, x + 5, y + 10, x + 5, y + 15);
                }

                if (tile.PossibleMoves.HasFlag(Enums.Moves.L))
                {
                    graphics.DrawLine(pen, x, y + 5, x - 5, y + 5);
                }

                if (tile.PossibleMoves.HasFlag(Enums.Moves.R))
                {
                    graphics.DrawLine(pen, x + 10, y + 5, x + 15, y + 5);
                }
            }

            bitmap.Save(Path.Combine(path, fileName));
        }
    }
}
