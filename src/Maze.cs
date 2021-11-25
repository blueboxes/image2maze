using image2maze.Models;
using System.Drawing;

namespace image2maze
{
    public class Maze
    {
        public Color[,] ColourMap { get; set; }
        public Maze()
        {

        }
          
        public void Generate(string sourceImagePath)
        {
            var mz = new MazeGenerator();
            var img = new MazeImageLoader();

            var imgGrid = img.LoadFromImage(sourceImagePath, out var colourMap);

            Grid = mz.Generate(imgGrid);
            ColourMap = colourMap;
            Start = null;
            End = null;
        }

        /// <summary>
        /// Grid of Data
        /// </summary>
        /// <remarks>See why we use a mutli-dimentional array over a jagged https://stackoverflow.com/a/45410114/33</remarks>
        public Direction[,] Grid { get; set; }
        public MazePoint Start { get; set; }
        public MazePoint End { get; set; } 
    }
 
}
