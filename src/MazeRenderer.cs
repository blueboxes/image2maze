using image2maze.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace image2maze
{
    public class MazeRenderer
    {
        public bool SaveToJpeg(Direction[,] grid, string path, RenderSettings settings)
        {
            var mazeHeight = grid.GetLength(0);
            var mazeWidth = grid.GetLength(1);
          
            var cellSize = settings.CellSize;
            var padding = settings.CellSize;
            var width = mazeWidth * cellSize + settings.PenWidth + padding * 2;
            var height = mazeHeight * cellSize + settings.PenWidth + padding * 2;

            using (var image = new Bitmap(width, height))
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(settings.BackgroundColour);

                    for (int row = 0; row < mazeHeight; row++)
                    {
                        for (int col = 0; col < mazeWidth; col++)
                        {
                            DrawCell(grid, row, col, graphics, settings);
                        }
                    }

                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;

                    graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                    image.Save(path, ImageFormat.Jpeg);
                }
            }
            return true;
        }

        private static void DrawCell(Direction[,] grid, int row, int col, Graphics graphics, RenderSettings settings)
        {
            var cellSize = settings.CellSize;

            using (var wallPen = new Pen(GetPenColour(settings, row, col)))
            using (var cellBrush = new SolidBrush(settings.ColourMap[row, col]))
            {
                wallPen.Width = 5;
                wallPen.EndCap = LineCap.Round;
                wallPen.StartCap = LineCap.Round;

                var xOffset = col * cellSize + settings.PenWidth / 2 + settings.CellSize;
                var yOffset = row * cellSize + settings.PenWidth / 2 + settings.CellSize;

                if (settings.FillBackGround)
                    graphics.FillRectangle(cellBrush, xOffset, yOffset, cellSize, cellSize);

                if (grid[row, col].HasFlag(Direction.N))
                    graphics.DrawLine(wallPen, new PointF(xOffset, yOffset), new Point(xOffset + cellSize, yOffset));

                if (grid[row, col].HasFlag(Direction.S))
                    graphics.DrawLine(wallPen, new PointF(xOffset, yOffset + cellSize), new Point(xOffset + cellSize, yOffset + cellSize));

                if (grid[row, col].HasFlag(Direction.E))
                    graphics.DrawLine(wallPen, new PointF(xOffset + cellSize, yOffset), new Point(xOffset + cellSize, yOffset + cellSize));

                if (grid[row, col].HasFlag(Direction.W))
                    graphics.DrawLine(wallPen, new PointF(xOffset, yOffset), new Point(xOffset, yOffset + cellSize));
            } 
        }

        private static Color GetPenColour(RenderSettings settings, int row, int col)
        {
            if (settings.ColourMap == null || settings.FillBackGround)
                return settings.WallColour;

            return settings.ColourMap[row, col];
        }
    }
}
