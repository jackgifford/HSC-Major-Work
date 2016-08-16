using System;
using System.Collections.Generic;
using System.Linq;
using MajorWork.Logic.Models;

namespace MajorWork.Logic.Services
{
    public class MazePlayService
    {
        private Mazepoints _currentPoint;
        private readonly Maze _maze;
        private readonly List<Mazepoints> _pathSolution;

        public MazePlayService(Maze maze)
        {
            _maze = maze;
            var mazePath = new List<Mazepoints>();

            _pathSolution = mazePath;
            _pathSolution.Add(new Mazepoints(0, 0, true)); //Add starting coords
        }

        public bool Gauntlet(Mazepoints postion, MoveList move)
        {
            if (!MoveSelection(postion, move)) return false;
            foreach (var item in _pathSolution) //Iterate through list if mazepoint is there 
            {
                if (item.X == _currentPoint.X && item.Y == _currentPoint.Y)
                {
                    RemovePath();
                }
            }

            _pathSolution.Add(new Mazepoints(_currentPoint.X, _currentPoint.Y, true)); 
            return true;
        }

        private bool MoveSelection(Mazepoints currentPoint, MoveList move)
        {
            _currentPoint = currentPoint;

            switch (move)
            {
                case MoveList.Up:
                    _currentPoint.Y -= 1;
                    if (MoveValidation() && _currentPoint.Y >= 0)
                        return true;
                    _currentPoint.Y += 1;
                    break;

                case MoveList.Left:
                    _currentPoint.X -= 1;
                    if (MoveValidation() && _currentPoint.X >= 0)
                        return true;
                    _currentPoint.X += 1;
                    break;

                case MoveList.Down:
                    _currentPoint.Y += 1;
                    if (MoveValidation() && _currentPoint.Y <= _maze.Length)
                        return true;
                    _currentPoint.Y -= 1;
                    break;

                case MoveList.Right:
                    _currentPoint.X += 1;
                    if (MoveValidation() && _currentPoint.X <= _maze.Length)
                        return true;                    
                    _currentPoint.X -= 1;
                    break;
            }
            return false;
        }

        private void RemovePath()
        {
            _pathSolution.RemoveAt(_pathSolution.Count - 1);
            //Remove positon from list
            //Draw new position
            //Return false
            
            throw new NotImplementedException();
        }

        private bool MoveValidation()
        {
            try
            {
                return _maze.MazeGrid.First(a => (a.X == (_currentPoint.X) && a.Y == (_currentPoint.Y))).IsPath;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
