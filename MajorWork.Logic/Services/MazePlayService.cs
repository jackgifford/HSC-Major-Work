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
        private maze mazeGrid;

        public MazePlayService(maze userMazeGrid)
        {
            mazeGrid = userMazeGrid;
            playermoves Fields = new playermoves();
        }

        public void MoveSelection()
        {
            throw new NotImplementedException();
        }

        private void MoveValdiation()
        {
            throw new NotImplementedException();
        }
    }
}
