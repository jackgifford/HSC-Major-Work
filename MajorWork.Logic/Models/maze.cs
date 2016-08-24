using System.Collections.Generic;

namespace MajorWork.Logic.Models
{
    public class Maze 
    {
        

        //TODO: Add validation to width, and length to ensure they are within range
        public int Width { get; set; }
        public int Length { get; set; } 
        public List<Stack> MazeStack { get; } = new List<Stack>();
        public List<Mazepoints> MazeGrid { get; } = new List<Mazepoints>();
    }
}
