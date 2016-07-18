using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorWork.Logic.Models
{
    class AStar
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int G { get; set; } //Cost from start node to this node
        public int H { get; set; } //Huesitic
        public int F { get; set; } // G + H
    }
}
