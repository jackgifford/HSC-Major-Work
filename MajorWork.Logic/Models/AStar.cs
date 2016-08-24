

namespace MajorWork.Logic.Models
{
    public class AStar
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int G { get; set; } //Cost from start node to this node
        public int H { get; set; } //Huesitic
        public int F { get; set; } // G + H
        public AStar Parent { get; set; }
    }
}
