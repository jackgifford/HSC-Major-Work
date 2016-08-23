

namespace MajorWork.Logic.Models
{
    public class Mazepoints
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsPath { get; set; }
        public bool IsSolution { get; set; }

        public Mazepoints(int userX, int userY, bool userWall, bool userSolution)
        {
            X = userX;
            Y = userY;
            IsPath = userWall;
            IsSolution = userSolution;
        }
    }
}
