using image2maze.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace image2maze
{
    /// <summary>
    /// Recursive Backtracking Based on https://people.cs.ksu.edu/~ashley78/wiki.ashleycoleman.me/index.php/Recursive_Backtracker.html  
    /// </summary>
    /// <remarks>
    /// Also see https://www.youtube.com/watch?v=JPQb6M31oc4 and http://weblog.jamisbuck.org/2010/12/27/maze-generation-recursive-backtracking
    /// </remarks>
    public class MazeGenerator
    {
        private const Direction AllDirections = Direction.E | Direction.S | Direction.W | Direction.N;
        readonly Dictionary<Direction, int> DX = new Dictionary<Direction, int>() {
                { Direction.E , 1},
                { Direction.N , 0},
                { Direction.W , -1},
                { Direction.S , 0}
            };

        readonly Dictionary<Direction, int> DY = new Dictionary<Direction, int>() {
                { Direction.E , 0},
                { Direction.N , -1},
                { Direction.W , 0},
                { Direction.S , 1}
            };

        readonly Dictionary<Direction, Direction> Reverse = new Dictionary<Direction, Direction>() {
                { Direction.E , Direction.W},
                { Direction.N , Direction.S},
                { Direction.W , Direction.E},
                { Direction.S , Direction.N}

            };
        protected Random random = new Random();

        public int Height { get; private set; }
        public int Width { get; private set; }

        public Direction[,] Generate(Direction[,] grid)
        {
            Height = grid.GetLength(0);
            Width = grid.GetLength(1);

            var start = FindStart(grid);
             return MoveFrom(start.Y, start.X, grid);
             //return MoveFrom(0, 0, grid);
        }

        private MazePoint FindStart(Direction[,] grid)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (grid[i, j].HasFlag(AllDirections))
                        return new MazePoint(j, i);
                }
            }

            return new MazePoint(0, 0);
        }
   
        private Direction[,] MoveFrom(int row, int col, Direction[,] grid)
        {
            var directions = GetDirections();

            foreach (var direction in directions)
            {
                var newCol = col + DX[direction];
                var newRow = row + DY[direction];

                if (IsAvailable(grid, newCol, newRow))
                {

                    grid[row, col] &= ~direction;
                    grid[newRow, newCol] &= ~Reverse[direction];

                    grid = MoveFrom(newRow, newCol, grid);
                }
            }
            return grid;
        }

        private IOrderedEnumerable<Direction> GetDirections()
        {
            return new[] { Direction.E, Direction.S, Direction.W, Direction.N }
                            .OrderBy(a => random.Next());
        }

        private bool IsAvailable(Direction[,] grid, int newCol, int newRow)
        {
            return newCol <= Width - 1 && newCol >= 0 && newRow <= Height - 1 && newRow >= 0 && grid[newRow, newCol] == AllDirections;
        }
    }
}