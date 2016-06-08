using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MajorWork.Logic.Services;
using MajorWork.Logic.Models;
using System.Windows.Controls;


namespace MajorWork.ViewModels
{
    public class MainWindowViewModel
    {
        private maze _maze;
        private MazeGenerationService _GenMaze;

        public void Generate(Grid blank)
        {
            maze maze = new maze();
            _maze = maze;
            MazeGenerationService GenMaze = new MazeGenerationService(10, 10, _maze); //Switch to custom width
            _GenMaze = GenMaze;

            Draw mazeGraphic = new Draw(_maze, blank);
        }

        public void Clear() 
        {
            _maze = null;
            _GenMaze = null;
            GC.Collect();
        }
    }
}
