using System;
using System.Collections.Generic;
using System.Linq;
using MajorWork.Logic.Models;
using MoreLinq;

namespace MajorWork.Logic.Services
{
    public class MazeSolveService
    {
        private List<AStar> OpenSet;
        private List<AStar> ClosedSet;
        private List<AStar> EntireMaze;
        private List<Mazepoints> mazeCoords;

        private int finalX;
        private int finalY;

        public MazeSolveService()
        {

            OpenSet.Add(new AStar //Add the starting position to the open list
            {
                X = 0,
                Y = 0
            });

            mazeCoords.ForEach(item => EntireMaze.Add(new AStar { X = item.X, Y = item.Y})); //Get all the nodes into a list that can be used by the AStar algorithm

            foreach (var child in EntireMaze) //Preprocess every heursitic in the maze but only process those that aren't walls
                child.H = HeuristicCalculator(child.X, child.Y, finalX, finalY);
            

            AStar(); //Galvanise
            BuildSolution();
        }

        private int HeuristicCalculator(int x, int y, int final1, int final2) //Based off Manhattan Distance
        {
            return Math.Abs(finalX - x) + Math.Abs(finalY - y);
        }

        private void AStar()
        {
            while (OpenSet.Count > 0)
            {
                var current = OpenSet.MinBy(x => x.F); //Return AStar object with the cheapest cost function in the openlist
                OpenSet.Remove(current);
                ClosedSet.Add(current);

                var neighbours = BuildNeighbourList(current);

                foreach (var neighbour in neighbours) 
                {
                    var cost = current.G + 1;

                    if (OpenSet.Contains(neighbour) && neighbour.G < current.G)
                        OpenSet.Remove(neighbour);

                    if (ClosedSet.Contains(neighbour) && neighbour.G < current.G)
                        ClosedSet.Remove(neighbour);
                    
                    if (!OpenSet.Contains(neighbour) && !ClosedSet.Contains(neighbour))
                    {
                        neighbour.G = cost;
                        OpenSet.Add(neighbour);
                    }
                }
            }
        }

        private IEnumerable<AStar> BuildNeighbourList(AStar current) 
        {
            return new List<AStar>(new[]
            {
                EntireMaze.First(x => (x.X == current.X) && (x.Y == current.Y + 1)), //Up
                EntireMaze.First(x => (x.X == current.X) && (x.Y == current.Y - 1)), //Down
                EntireMaze.First(x => (x.X == current.X - 1) && (x.Y == current.Y)), //Left
                EntireMaze.First(x => (x.X == current.X + 1) && (x.Y == current.Y)) //Right
            });
        } 

        private void BuildSolution()
        {

        }
    }
}
