using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MajorWork.Logic.Models;
using MoreLinq;

namespace MajorWork.Logic.Services
{
    public class MazeSolveService
    {
        private List<AStar> _openSet;
        private List<AStar> _closedSet;
        private List<AStar> _entireMaze;
        private List<Mazepoints> _mazeCoords;
        private int _length; //Update with a real value
        private AStar _target;

        public List<AStar> IterableList;
        public List<AStar> Solution;

        public MazeSolveService(List<Mazepoints> mazeCoords)
        {
            _mazeCoords = mazeCoords;
            _entireMaze = new List<AStar>();
            _openSet = new List<AStar>();
            IterableList = new List<AStar>();
            _closedSet = new List<AStar>();
            _target = new AStar
            {
                X = 8,
                Y = 8
            };

            //Fix these hardcoded values once AStar is properly implmented


            foreach (var child in mazeCoords) //Get all the nodes into a list that can be used by the AStar algorithm
            {
                if (child.IsPath)
                {
                   _entireMaze.Add(new AStar {X = child.X, Y = child.Y});
                }
            }

            

            foreach (var child in _entireMaze) //Preprocess every heursitic in the maze but only process those that aren't walls
                child.H = HeuristicCalculator(child.X, child.Y);

            _length = _entireMaze.Max(x => x.Y);
            var CurrentPosition = AStarDiff(); //Galvanise
            BuildSolution(CurrentPosition);

        }

        private int HeuristicCalculator(int x, int y) //Based off Manhattan Distance
        {
            return Math.Abs(_target.X - x) + Math.Abs(_target.Y - y);
        }

        private AStar AStarDiff()
        {
            var h = HeuristicCalculator(0, 0);
            var f = h + 0;
            var start = new AStar
            {
                X = 0,
                Y = 0,
                G = 0,
                H = h,
                F = f
            };

            _openSet.Add(start);

            AStar FinalPosition = new AStar(); 

            while (_openSet.Count > 0)
            {
                var current = _openSet.MinBy(x => x.F); //Find the cheapest value in the openset and make it the current

                if (current.X == _target.X && current.Y == _target.Y)  //path found
                {
                    FinalPosition = current;
                    break;
                }

                _openSet.Remove(current);
                _closedSet.Add(current);


                var neighbours = BuildNeighbourList(current);

                //Tidier to handle the below in the BuildNieghbourList function
                //foreach (var neighbour in neighbours)
                //{
                //    neighbour.G = current.G + 1;
                //    neighbour.Parent = current;
                //    neighbour.F = neighbour.G + HeuristicCalculator(neighbour.X, neighbour.Y);
                //}

                //Finds the four neighbours to current, and stores them in a list

                foreach (var neighbour in neighbours)
                {

                    //if (neighbour.X == _target.X && neighbour.Y == _target.Y)
                    //{
                    //    FinalPosition = neighbour;
                    //}
                    
                    
                    //Find the x that corresponds with neighbour in the list and replace it
                    if (_closedSet.Exists(x => (x.X == neighbour.X) && x.Y == neighbour.Y) == false) //If not in closed list
                    {
                        neighbour.F = neighbour.G + HeuristicCalculator(neighbour.X, neighbour.Y);

                        if (_openSet.Exists(x => (x.X == neighbour.X) && x.Y == neighbour.Y) == false) //If not in open list
                        {
                            _openSet.Add(neighbour);
                            
                            Debug.WriteLine("Added {0}, {1}", neighbour.X, neighbour.Y);
                            Debug.WriteLine("Parent coords are:  {0} {1} ", neighbour.Parent.X, neighbour.Parent.Y);
                        }

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
            return FinalPosition;
        }

        private List<AStar> BuildNeighbourList(AStar current) //If statements make sure the neighbour is within range of the grid and that no wall exists
        {
            var list = new List<AStar>();

            if (current.Y - 1 >= 0 && _mazeCoords.First(x => (x.X == current.X) && (x.Y == current.Y - 1)).IsPath)
            {
                list.Add(AddNeighbourdata(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y - 1)), current));
            }
                          
            if (current.Y + 1 <= _length && _mazeCoords.First(x => (x.X == current.X ) && (x.Y == current.Y + 1)).IsPath)
            {
                list.Add(AddNeighbourdata(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y + 1)), current));
            }
                
            if (current.X - 1 >= 0 && _mazeCoords.First(x => (x.X == current.X - 1) && (x.Y == current.Y)).IsPath)
            {
                list.Add(AddNeighbourdata(_entireMaze.First(x => (x.X == current.X - 1) && (x.Y == current.Y)), current));
            }
               
            if (current.X + 1 <= _length && _mazeCoords.First(x => (x.X == current.X + 1) && (x.Y == current.Y)).IsPath)
            {
                list.Add(AddNeighbourdata(_entireMaze.First(x => (x.X == current.X + 1) && (x.Y == current.Y)), current));
            }
                
            return list;
        }
        //Builds a list that goes from target location to the entry point of the maze

        private AStar AddNeighbourdata (AStar neighbour, AStar current)
        {
            AStar neighbourEntry = new AStar();
            neighbourEntry.State = neighbour.State;
            neighbourEntry.X = neighbour.X;
            neighbourEntry.Y = neighbour.Y;
            neighbourEntry.H = neighbour.H;
            neighbourEntry.G = current.G + 1;
            neighbourEntry.Parent = current;
            neighbourEntry.F = neighbourEntry.G + HeuristicCalculator(neighbourEntry.X, neighbourEntry.Y);
            return neighbourEntry;
        }

        private List<AStar> BuildSolution(AStar finalPos) 
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
