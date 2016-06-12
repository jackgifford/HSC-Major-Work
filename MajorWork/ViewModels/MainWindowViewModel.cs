using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MajorWork.Logic.Services;
using MajorWork.Logic.Models;
using System.Windows.Controls;
using System.Windows.Input;


namespace MajorWork.ViewModels
{
    public class MainWindowViewModel
    {
        private maze _maze;
        private MazeGenerationService _GenMaze;
        private Draw _drawLibrary;
        private mazepoints _position;
        private MazePlayService _play;

        public void Generate(Grid blank)
        {
            maze maze = new maze();
            _maze = maze;
            MazeGenerationService GenMaze = new MazeGenerationService(10, 10, _maze); //Switch to custom width
            _GenMaze = GenMaze;

            Draw mazeGraphic = new Draw(_maze, blank);
            _drawLibrary = mazeGraphic;

            MazePlayService play = new MazePlayService(_maze);
            _play = play;

            mazepoints position = new mazepoints(0, 0, false, false);
            _position = position;
        }

        public void Play(Grid blank, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (_play.Gauntlent(_position, moveList.Up))
                        _drawLibrary.DrawPath(_position);
                    else
                        _drawLibrary.RemovePath(_position);
                    break;
                case Key.Down:
                    if (_play.Gauntlent(_position, moveList.Down))
                        _drawLibrary.DrawPath(_position);
                    else
                        _drawLibrary.RemovePath(_position);
                    break;
                case Key.Left:
                    if (_play.Gauntlent(_position, moveList.Left))
                        _drawLibrary.DrawPath(_position);
                    else
                        _drawLibrary.RemovePath(_position);
                    break;
                case Key.Right:

                    if (_play.Gauntlent(_position, moveList.Right))
                        _drawLibrary.DrawPath(_position);
                    else
                        _drawLibrary.RemovePath(_position);
                    break;

            } 
        }

        public void Clear() 
        {
            _maze = null;
            _GenMaze = null;
            _position = null;
        }
    }
}
