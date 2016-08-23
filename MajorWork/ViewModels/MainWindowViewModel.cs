using System.Windows.Controls;
using System.Windows.Input;
using MajorWork.Logic.Helpers;
using MajorWork.Logic.Models;
using MajorWork.Logic.Services;

namespace MajorWork.ViewModels
{
    public class MainWindowViewModel
    {
        public delegate void ProgressUpdate(int value);

        private Draw _drawLibrary;
        private Mazepoints _finalCoords;
        private MazeGenerationService _genMaze;
        private Maze _maze;
        private MazePlayService _play;
        private Mazepoints _position;
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

            var position = new Mazepoints(0, 0, false, true);
            _position = position;
        }

        public void FindExitPoint()
        {
            var flag = true;
            while (flag)
            {
                var testX = MathRandom.GetRandomNumber(0, _maze.Length);
                var testY = MathRandom.GetRandomNumber(0, _maze.Length);

                if (_maze.MazeGrid.Exists(x => x.X == testX && x.Y == testY && x.IsPath))
                {
                    _finalCoords = new Mazepoints(testX, testY, true, false);
                    flag = false;
                }
            }
        }

        public void DrawGrid(Grid blank, byte r, byte g, byte b)
        {
            var mazeGraphic = new Draw(_maze, blank, _finalCoords);
            _drawLibrary = mazeGraphic;
        }

        public void Play(KeyEventArgs e)
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
            var solver = new MazeSolveService(_maze.MazeGrid, _finalCoords);
            _drawLibrary.DrawSolution(solver.Solution);
        }

        public void Clear()
        {
            _maze = null;
            _genMaze = null;
            _position = null;
        }
    }
}