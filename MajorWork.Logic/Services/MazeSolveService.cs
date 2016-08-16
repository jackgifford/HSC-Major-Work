using System;
using System.Collections.Generic;
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
        public List<AStar> _solution;
        private List<Mazepoints> _mazeCoords;

        private int _length; //Update with a real value

        private readonly AStar _startPoint;
        private readonly AStar _target;
    

        public MazeSolveService(List<Mazepoints> mazeCoords)
        {
            _mazeCoords = mazeCoords;
            _entireMaze = new List<AStar>();
            _openSet = new List<AStar>();
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

            _openSet.Add(_entireMaze.First(x => (x.X == 0) && (x.Y == 0)));

            foreach (var child in _entireMaze) //Preprocess every heursitic in the maze but only process those that aren't walls
                child.H = HeuristicCalculator(child.X, child.Y);

            _length = _entireMaze.Max(x => x.Y);
            AStar(); //Galvanise
            _solution = BuildSolution(); 
        }

        private int HeuristicCalculator(int x, int y) //Based off Manhattan Distance
        {
            return Math.Abs(_target.X - x) + Math.Abs(_target.Y - y);
        }

        private void AStar()
        {
            while (_openSet.Count > 0 || _closedSet.Contains(_target))
            {
                var current = _openSet.MinBy(x => x.F); //Return AStar object with the cheapest cost function in the openlist, using MoreLinq because I didn't know how to implement this
                _openSet.Remove(current);
                _closedSet.Add(current);

                var neighbours = BuildNeighbourList(current);

                foreach (var neighbour in neighbours) 
                {
                    var cost = current.G + 1;

                    //If it's in closed list ignore it

                    if (!_openSet.Contains(neighbour))
                    {
                        _openSet.Add(neighbour);
                        neighbour.Parent = current;
                        neighbour.G = cost;
                        neighbour.F = neighbour.G + neighbour.H;
                        //Calculate F, G & H
                    }

                    if (_openSet.Contains(neighbour) && neighbour.G < current.G)
                    {
                        neighbour.Parent = current;
                        neighbour.G = cost;
                        neighbour.F = neighbour.G + neighbour.H;
                    }
                }
            }
        }

        private IEnumerable<AStar> BuildNeighbourList(AStar current) //Implement tests to make sure neighbours are within range
        {
            var list = new List<AStar>(); //Crashes as soon as it tries to find a wall

            if (current.Y - 1 >= 0 )
                list.Add(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y - 1)));

            if (current.Y + 1 <= _length)
                list.Add(_entireMaze.First(x => (x.X == current.X) && (x.Y == current.Y + 1)));

            if (current.X - 1 >= 0)

                list.Add(_entireMaze.First(x => (x.X == current.X - 1) && (x.Y == current.Y)));

            if (current.X + 1 <= _length)
                list.Add(_entireMaze.First(x => (x.X == current.X + 1) && (x.Y == current.Y)));

            return list;
        } 

        private List<AStar> BuildSolution() //Builds a list that goes from target location to the entry point of the maze
        {
            var path = new List<AStar>();
            var current = _target; //Start at the target

            while (current != _startPoint)
            {
                path.Add(current);
                current = current.Parent;
            }

            return path;
        }
    }
}
