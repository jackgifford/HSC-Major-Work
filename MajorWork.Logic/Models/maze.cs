using System.Collections.Generic;

namespace MajorWork.Logic.Models
{
    public class Maze 
    {
        private int _width;
        private int _length;
        private List<Stack> _mazeStack = new List<Stack>();
        private List<Mazepoints> _mazePoints = new List<Mazepoints>();


        //TODO: Add validation to width, and length to ensure they are within range
        public int Width { get; set; }
        public int Length { get; set; } 
        public List<Stack> MazeStack { get { return _mazeStack; } set { _mazeStack = MazeStack; } }
        public List<Mazepoints> MazeGrid { get { return _mazePoints; } set { _mazePoints = MazeGrid; } }
    }
}
