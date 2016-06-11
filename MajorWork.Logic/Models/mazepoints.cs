using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorWork.Logic.Models
{
    public class mazepoints
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool isPath { get; set; }

        public mazepoints(int userX, int userY, bool userWall)
        {
            X = userX;
            Y = userY;
            isPath = userWall;
        }
    }
}
