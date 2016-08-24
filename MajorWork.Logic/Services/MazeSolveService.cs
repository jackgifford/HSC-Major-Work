using System;
using System.Collections.Generic;
using System.Linq;
using MajorWork.Logic.Models;
using MoreLinq;

namespace MajorWork.Logic.Services
{
    public class MazeSolveService
    {
        private readonly List<AStar> _openSet;
        private readonly List<AStar> _closedSet;
        private readonly List<AStar> _entireMaze;
        private readonly List<Mazepoints> _mazeCoords;
        private readonly int _length; //Update with a real value
        private readonly AStar _target;

        public readonly List<AStar> Solution;

        public MazeSolveService(List<Mazepoints> mazeCoords, Mazepoints finalCoords)
        {
            _mazeCoords = mazeCoords;
            _entireMaze = new List<AStar>();
            _openSet = new List<AStar>();
            _closedSet = new List<AStar>();
            _target = new AStar
            {
                X = finalCoords.X,
                Y = finalCoords.Y
            };

            //Fix these hardcoded values once AStar is properly implmented


            foreach (var child in mazeCoords) //Get all the nodes into a list that can be used by the AStar algorithm
            {
                if (child.IsPath)
                {
                    _entireMaze.Add(new AStar { X = child.X, Y = child.Y });
                }
            }


            _length = _entireMaze.Max(x => x.Y); //Length calculated by finding the largest Y coordinate in the list

            var currentPosition = GenerateSolution();

            if (currentPosition != null)
            {
                Solution = BuildSolution(currentPosition);
            }
            else
            {
                //No solution is possible
            }

        }

        private int HeuristicCalculator(int x, int y) //Based off Manhattan Distance
        {
            return Math.Abs(_target.X - x) + Math.Abs(_target.Y - y);
        }

        private AStar GenerateSolution()
        {
            var h = HeuristicCalculator(0, 0);
            var f = h + 0;
            var start = new AStar { X = 0, Y = 0, G = 0, H = h, F = f };

            _openSet.Add(start);

            var finalPosition = new AStar();

            while (_openSet.Count > 0)
            {
                var current = _openSet.MinBy(x => x.F); //Find the cheapest value in the openset and make it the current

                if (current.X == _target.X && current.Y == _target.Y)  //path found
                {
                    finalPosition = current;
                    break;
                }

                _openSet.Remove(current);
                _closedSet.Add(current);

                var neighbours = BuildNeighbourList(current); //Finds the four neighbours to current, and stores them in a list

                foreach (var neighbour in neighbours)
                {

                    //Find the x that corresponds with neighbour in the list and replace it
                    if (_closedSet.Exists(x => (x.X == neighbour.X) && x.Y == neighbour.Y) == false) //If not in closed list
                    {
                        neighbour.F = neighbour.G + HeuristicCalculator(neighbour.X, neighbour.Y);

                        if (_openSet.Exists(x => (x.X == neighbour.X) && x.Y == neighbour.Y) == false) //If not in open list
                            _openSet.Add(neighbour);

                        else
                        {
                            var openNeighbour = _openSet.First(x => x.X == neighbour.X && x.Y == neighbour.Y);

                            if (neighbour.G < openNeighbour.G)
                            {
                                openNeighbour.Parent = neighbour.Parent;
                                openNeighbour.G = neighbour.G;
                            }
                        }
                    }

                }

            }
            //Need to handle if comes here without finding a final position - i.e. no path exists
            return finalPosition;
        }

        private IEnumerable<AStar> BuildNeighbourList(AStar current) //If statements make sure the neighbour is within range of the grid and that no wall exists 
        {
            var list = new List<AStar>();

            if (current.Y - 1 >= 0 && _mazeCoords.First(x => (x.X == current.X) && (x.Y == current.Y - 1)).IsPath)
                list.Add(AddNeighbourData(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y - 1)), current));

            if (current.Y + 1 <= _length && _mazeCoords.First(x => (x.X == current.X) && (x.Y == current.Y + 1)).IsPath)
                list.Add(AddNeighbourData(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y + 1)), current));


            if (current.X - 1 >= 0 && _mazeCoords.First(x => (x.X == current.X - 1) && (x.Y == current.Y)).IsPath)
                list.Add(AddNeighbourData(_entireMaze.First(x => (x.X == current.X - 1) && (x.Y == current.Y)), current));

            if (current.X + 1 <= _length && _mazeCoords.First(x => (x.X == current.X + 1) && (x.Y == current.Y)).IsPath)
                list.Add(AddNeighbourData(_entireMaze.First(x => (x.X == current.X + 1) && (x.Y == current.Y)), current));

            return list;
        }


        private AStar AddNeighbourData(AStar neighbour, AStar current) //Stack and heap memory
        {
            //Note to Jack. Need to be careful when copying classes as you don't make a copy you actually just copy the reference. So AStar B = AStar A means that any changes you make to B will also make the same change to A. 
            //Read up and understand the difference between stack and heap memory - is important.
            AStar neighbourEntry = new AStar
            { 
                X = neighbour.X,
                Y = neighbour.Y,
                H = neighbour.H,
                G = current.G + 1,
                Parent = current,
            };
            neighbourEntry.F = neighbourEntry.G + HeuristicCalculator(neighbourEntry.X, neighbourEntry.Y);
            return neighbourEntry;
        }

        private static List<AStar> BuildSolution(AStar finalPos)
        {
            var list = new List<AStar>();

            while (finalPos.Parent != null)
            {
                list.Add(finalPos);
                finalPos = finalPos.Parent;
            }

            return list;
        }
    }
}
