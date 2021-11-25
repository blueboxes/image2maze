using System.Drawing;

namespace image2maze.Models
{
    public class RenderSettings
    {
        public int CellSize { get; set; } = 50;
        public int PenWidth { get; set; } = 5;
        public Color BackgroundColour { get; set; } = Color.FromArgb(242, 242, 242);
        public Color WallColour { get; set; } = Color.Black;
        public bool FillBackGround { get; set; }
        public Color[,] ColourMap { get; set; }
    }
}
