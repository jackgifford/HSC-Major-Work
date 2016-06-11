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

        public MazePlayService(maze maze)
        {
            _maze = maze;
            playermoves Fields = new playermoves();
        }

        public void Gauntlent(moveList move)
        {
            if (true)
                MoveSelection(_currentPoint, move);

            if (false)
                removePath();

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

        public void removePath()
        {
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
