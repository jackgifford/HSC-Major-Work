using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
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
        private readonly AStar _startPoint;
        private AStar _target;

        public List<AStar> _iterableList;
        public List<AStar> _solution;

        public MazeSolveService(List<Mazepoints> mazeCoords)
        {
            _mazeCoords = mazeCoords;
            _entireMaze = new List<AStar>();
            _openSet = new List<AStar>();
            _iterableList = new List<AStar>();
            _closedSet = new List<AStar>();
            _target = new AStar
            {
                X = 8,
                Y = 8
            };

            _startPoint = new AStar
            {
                X = 0,
                Y = 0,
                F = 0,
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
            AStarDiff(); //Galvanise

        }

        private int HeuristicCalculator(int x, int y) //Based off Manhattan Distance
        {
            return Math.Abs(_target.X - x) + Math.Abs(_target.Y - y);
        }

        private void AStarDiff()
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

            while (_openSet.Count > 0)
            {
                var current = _openSet.MinBy(x => x.F); //Find the cheapest value in the openset and make it the current

                if (current.X == _target.X && current.Y == _target.Y)
                {
                    
                    _solution = BuildSolution(current);
                }


                _openSet.Remove(current);
                _closedSet.Add(current);


                var neighbours = BuildNeighbourList(current);

                foreach (var neighbour in neighbours)
                {
                    neighbour.G = current.G + 1;
                    neighbour.Parent = current;
                    neighbour.F = neighbour.G + HeuristicCalculator(neighbour.X, neighbour.Y);
                }

                //Finds the four neighbours to current, and stores them in a list

                foreach (var neighbour in neighbours)
                {

                    //Find the x that corresponds with neighbour in the list and replace it
                    if (_closedSet.Exists(x => (x.X == neighbour.X) && x.Y == neighbour.Y) == false) //If not in closed list
                    {
                        neighbour.F = neighbour.G + HeuristicCalculator(neighbour.X, neighbour.Y);

                        if (_openSet.Exists(x => (x.X == neighbour.X) && x.Y == neighbour.Y) == false) //If not in open list
                        {
                            _openSet.Add(neighbour);
                            _iterableList.Add(neighbour);
                            if (neighbour.X == 8 && neighbour.Y == 8)
                            {
                                
                                
                            }
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
        }

        private List<AStar> BuildNeighbourList(AStar current) //If statements make sure the neighbour is within range of the grid and that no wall exists
        {
            var list = new List<AStar>(); 

            if (current.Y - 1 >= 0 && _mazeCoords.First(x => (x.X == current.X) && (x.Y == current.Y - 1) ).IsPath) //Up
                list.Add(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y - 1)));

            if (current.Y + 1 <= _length && _mazeCoords.First(x => (x.X == current.X ) && (x.Y == current.Y + 1)).IsPath) //Down
                list.Add(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y + 1)));

            if (current.X - 1 >= 0 && _mazeCoords.First(x => (x.X == current.X - 1) && (x.Y == current.Y)).IsPath) //Left
                list.Add(_entireMaze.First(x => (x.X == current.X - 1) && (x.Y == current.Y)));

            if (current.X + 1 <= _length && _mazeCoords.First(x => (x.X == current.X + 1) && (x.Y == current.Y)).IsPath) //Right
                list.Add(_entireMaze.First(x => (x.X == current.X + 1) && (x.Y == current.Y)));

            return list;
        }
        //Builds a list that goes from target location to the entry point of the maze

        private List<AStar> BuildSolution(AStar finalPos) 
        {
            var list = new List<AStar>();
            var start = _iterableList.First();


            while (finalPos.Parent != null)
            {
                list.Add(finalPos);
                finalPos = finalPos.Parent;
            }

            return list;
        }
    }
}
