namespace image2maze.Models
{
    public record MazePoint
    {
        public MazePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

    }
}
