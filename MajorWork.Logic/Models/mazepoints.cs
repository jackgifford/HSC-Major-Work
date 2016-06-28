

namespace MajorWork.Logic.Models
{
    public class Mazepoints
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsPath { get; set; }

        public Mazepoints(int userX, int userY, bool userWall)
        {
            X = userX;
            Y = userY;
            IsPath = userWall;
        }
    }
}
