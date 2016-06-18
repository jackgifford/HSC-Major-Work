using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MajorWork.Logic.Models;

namespace MajorWork.Logic.Services
{
    public class MazePlayService
    {
        private mazepoints _currentPoint;
        private maze _maze;
        private List<mazepoints> _pathSolution;

        public MazePlayService(maze maze)
        {
            _maze = maze;
            playermoves Fields = new playermoves();
            List<mazepoints> mazePath = new List<mazepoints>();

            _pathSolution = mazePath;
            _pathSolution.Add(new mazepoints(0, 0, true)); //Add starting coords
        }

        public bool Gauntlet(mazepoints postion, moveList move)
        {
            if (MoveSelection(postion, move)) //Checks the move can be made
            {
               
                foreach (var item in _pathSolution) //Iterate through list if mazepoint is there 
                {
                    if (item.X == _currentPoint.X && item.Y == _currentPoint.Y)
                    {
                        RemovePath();
                    }
                }

                _pathSolution.Add(new mazepoints(_currentPoint.X, _currentPoint.Y, true)); 
                return true;
            }

            return false;

        }

        public bool MoveSelection(mazepoints currentPoint, moveList move)
        {
            _currentPoint = currentPoint;

            switch (move)
            {
                case moveList.Up:
                    _currentPoint.Y -= 1;
                    if (MoveValidation() && _currentPoint.Y >= 0)
                        return true;
                    _currentPoint.Y += 1;
                    break;

                case moveList.Left:
                    _currentPoint.X -= 1;
                    if (MoveValidation() && _currentPoint.X >= 0)
                        return true;
                    _currentPoint.X += 1;
                    break;

                case moveList.Down:
                    _currentPoint.Y += 1;
                    if (MoveValidation() && _currentPoint.Y <= _maze.length)
                        return true;
                    _currentPoint.Y -= 1;
                    break;

                case moveList.Right:
                    _currentPoint.X += 1;
                    if (MoveValidation() && _currentPoint.X <= _maze.length)
                        return true;                    
                    _currentPoint.X -= 1;
                    break;
            }
            return false;
        }

        public bool RemovePath()
        {
            //Remove positon from list
            //Draw new position
            //Return false
            
            throw new NotImplementedException();
        }

        public bool MoveValidation()
        {
            try
            {
                if (_maze.mazeGrid.Where(a => (a.X == (_currentPoint.X) && a.Y == (_currentPoint.Y))).First().isPath == true)
                    return true;
                //Allow move, else do nothing, and draw new position
                return false;
            }
            catch (Exception)
            {
                
                
            }

            return false;
        }
    }
}
