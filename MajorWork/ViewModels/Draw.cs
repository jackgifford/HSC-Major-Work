using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using MajorWork.Logic.Models;
using MajorWork.Logic.Services;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Drawing;


namespace MajorWork.ViewModels
{
    class Draw
    {
        private maze maze;
        private Grid grid;

        public Draw(maze mazeGrid, Grid blank)
        {
            maze = mazeGrid;
            grid = blank;
            DrawMaze();
            readyPlayer();
        }

        private void DrawMaze()
        {
            maze.mazeGrid.ForEach(delegate(mazepoints s)
            {
                if (s.isWall == true)
                {
                    GenerateRectangle(s);
                } 
            });
        }

        private void readyPlayer()
        {
            mazepoints startCoord = new mazepoints(0, 0, false);
            Rectangle startRect = DrawRect(76, 175, 80);
            AddChildToGrid(startRect, startCoord);

            mazepoints finalCoord = new mazepoints(8, 8, false);
            Rectangle finalRect = DrawRect(244, 67, 54);
            AddChildToGrid(finalRect, finalCoord);
        }

        private void GenerateRectangle(mazepoints s)
        {
            var myRect = DrawRect(255, 255, 255);
            AddChildToGrid(myRect, s);
        }

        private Rectangle DrawRect( byte r, byte g, byte b)
        {
            Rectangle myRect = new Rectangle();
            myRect.Width = 50;
            myRect.Height = 50;
            SolidColorBrush colourBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
            
            myRect.Fill = colourBrush;
            return myRect;
        }

        private void AddChildToGrid(Rectangle val, mazepoints s)
        {
            grid.Children.Add(val);
            Grid.SetRow(val, s.Y);
            Grid.SetColumn(val, s.X);
        }
    }
}
