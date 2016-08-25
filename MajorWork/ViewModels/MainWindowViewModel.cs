using System;
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
        private MazeSolveService _solution;
        private Maze _maze;
        private MazePlayService _play;
        private Mazepoints _position;

        public event ProgressUpdate OnProgressUpdate;

        public void Generate(int userLength)
        {
            var maze = new Maze();
            _maze = maze;
            if (OnProgressUpdate != null)
            {
                OnProgressUpdate.Invoke(20); //Updates the loading bar on the MainWindow
            }

            var genMaze = new MazeGenerationService(userLength, userLength, _maze);
            _genMaze = genMaze;

            if (OnProgressUpdate != null)
            {
                OnProgressUpdate.Invoke(70); //Updates the loading bar on the MainWindow
            } 

            var play = new MazePlayService(_maze);
            _play = play;

            if (OnProgressUpdate != null)
            {
                OnProgressUpdate.Invoke(10); //Updates the loading bar on the MainWindow
            }

            CreateExitPoint();

            var position = new Mazepoints(0, 0, false, true);
            _position = position;
        }

        private void CreateExitPoint() //Creates a random end point to the maze in the bottom right quadrant of the maze
        {
            var flag = true;

            var findMaze = (_maze.Length) - (_maze.Length/4);
            while (flag)
            {
                var testX = MathRandom.GetRandomNumber(findMaze, (_maze.Length -1));
                var testY = MathRandom.GetRandomNumber(findMaze, (_maze.Length - 1));

                if (_maze.MazeGrid.Exists(x => x.X == testX && x.Y == testY && x.IsPath))
                {
                    _finalCoords = new Mazepoints(testX, testY, true, false);
                    flag = false;
                }
            }
        }

        public void DrawGrid(Grid blank)
        {
            var mazeGraphic = new Draw(_maze, blank, _finalCoords);
            _drawLibrary = mazeGraphic;
        }

        public bool Play(KeyEventArgs e)
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

            if (_position.X == _finalCoords.X & _position.Y == _finalCoords.Y)
                return true; //Game won

            return false;
        }


        public void Solve()
        {
            var solver = new MazeSolveService(_maze.MazeGrid, _finalCoords);
            _solution = solver;
            
        }

        public void DrawSolution() //Takes the solution path and draws the solution over the grid
        {
            _drawLibrary.DrawSolution(_solution.Solution);
        }


        public void Clear() //Ensures that everything is cleared before a new maze is generated
        {
            _maze = null;
            _genMaze = null;
            _position = null;
        }
    }
}