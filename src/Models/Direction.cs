using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image2maze.Models
{
  
    [Flags]
    public enum Direction
    {
        None = 1 << 0,
        N = 1 << 1,
        S = 1 << 2,
        E = 1 << 3,
        W = 1 << 4
    }
}
