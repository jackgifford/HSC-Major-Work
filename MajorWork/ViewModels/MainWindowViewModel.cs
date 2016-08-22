using System.ComponentModel;
using MajorWork.Logic.Services;
using MajorWork.Logic.Models;
using MajorWork.Logic.Helpers;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Configuration;


namespace MajorWork.ViewModels
{
    public class MainWindowViewModel
    {
        private Maze _maze;
        private MazeGenerationService _genMaze;
        private Draw _drawLibrary;
        private Mazepoints _position;
        private Mazepoints _finalCoords;
        private MazePlayService _play;


        public delegate void ProgressUpdate(int value);
        public event ProgressUpdate OnProgressUpdate;

        public void Generate(Grid blank, int userLength)
        {
            var maze = new Maze();
            _maze = maze;
            if (OnProgressUpdate != null) //Can still be called without subscription to an event
                OnProgressUpdate(20);

            var genMaze = new MazeGenerationService(userLength, userLength, _maze);
            _genMaze = genMaze;

            if (OnProgressUpdate != null)
                OnProgressUpdate(70);

            var play = new MazePlayService(_maze);
            _play = play;

            if (OnProgressUpdate != null)
                OnProgressUpdate(10);

            FindExitPoint();

            var position = new Mazepoints(0, 0, false);
            _position = position;

        }

        public void FindExitPoint()
        {
            bool flag = true;
            while (flag)
            {
                int testX = MathRandom.GetRandomNumber(0, _maze.Length);
                int testY = MathRandom.GetRandomNumber(0, _maze.Length);

                if (_maze.MazeGrid.Exists(x => x.X == testX && x.Y == testY && x.IsPath == true))
                {
                    _finalCoords = new Mazepoints(testX, testY, true);
                    flag = false;
                }

            }
        }

        public void DrawGrid(Grid blank, int userLength)
        {
            var mazeGraphic = new Draw(_maze, blank, _finalCoords);
            _drawLibrary = mazeGraphic;
        }

        public void Play(Grid blank, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (_play.Gauntlet(_position, MoveList.Up))
                        _drawLibrary.DrawPath(_position);
                    break;
                case Key.Down:
                    if (_play.Gauntlet(_position, MoveList.Down))
                        _drawLibrary.DrawPath(_position);
                    break;
                case Key.Left:
                    if (_play.Gauntlet(_position, MoveList.Left))
                        _drawLibrary.DrawPath(_position);
                    break;
                case Key.Right:
                    if (_play.Gauntlet(_position, MoveList.Right))
                        _drawLibrary.DrawPath(_position);
                    break;
            }
        }

        public void Solve()
        {
            try
            {
                var solver = new MazeSolveService(_maze.MazeGrid, _finalCoords);
                _drawLibrary.DrawSolution(solver.Solution);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public void Clear()
        {
            _maze = null;
            _genMaze = null;
            _position = null;
        }

      
    }
}
