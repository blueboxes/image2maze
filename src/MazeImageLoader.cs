using image2maze.Models;
using System;
using System.Drawing;

namespace image2maze
{
    public class MazeImageLoader
    {
        private const int Transparent = 0;
        private const Direction AllDirections = Direction.E | Direction.S | Direction.W | Direction.N;

        public Direction[,] LoadFromImage(string path, out Color[,] colourMap)
        {
            using (var img = new Bitmap(path))
            {
                var Height = img.Height;
                var Width = img.Width;

                if (Height > 500 || Width > 500)
                    throw new ArgumentException("Image is to large, it must be less then 500px by 500px");

                var grid = new Direction[Height, Width];
                colourMap = new Color[Height, Width];

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Color pixel = img.GetPixel(j, i);
                        if (pixel.A == Transparent)
                        {
                            grid[i, j] = Direction.None;
                        }
                        else
                        {
                            colourMap[i, j] = Color.FromArgb(pixel.R, pixel.G, pixel.B);//remove alpha
                            grid[i, j] = AllDirections;
                        }
                    }
                }

                return grid;
            }
        }

    }
}
