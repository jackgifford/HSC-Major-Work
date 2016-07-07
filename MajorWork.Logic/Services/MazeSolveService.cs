using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajorWork.Logic.Models;

namespace MajorWork.Logic.Services
{
    public class MazeSolveService
    {
        private List<Mazepoints> OpenSet;
        private List<Mazepoints> ClosedSet;

        public MazeSolveService()
        {

            OpenSet.Add(new Mazepoints(0, 0, false)); //Add starting position to the open list
            //Call HuersticCalculator for every node in the list
            //But only process those that aren't walls
        }

        private void HeuristicCalculator(int x, int y, int finalX, int finalY) //Based off Manhattan Distance
        {
            var Manhattan = new Heuristic();
            Manhattan.DeltaX = Math.Abs(x - finalX);
            Manhattan.DeltaY = Math.Abs(y - finalY);
            Manhattan.H = (Manhattan.DeltaX + Manhattan.DeltaY);
        }

        private void AStar()
        {

        }

        private void BuildSolution()
        {

        }
    }
}
