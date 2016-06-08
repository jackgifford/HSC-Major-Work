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
            GenerateRectangle(startCoord);
        }

        private void GenerateRectangle(mazepoints s)
        {
            Rectangle myRect = new Rectangle();
            myRect = DrawRect(s, myRect);
            grid.Children.Add(myRect);
            Grid.SetRow(myRect, s.Y);
            Grid.SetColumn(myRect, s.X);
        }

        private Rectangle DrawRect(mazepoints s, Rectangle rect)
        {
            rect.Width = 50;
            rect.Height = 50;
            SolidColorBrush colourBrush = new SolidColorBrush(Color.FromRgb(255,255,255));
            
            rect.Fill = colourBrush;
            return rect;
        }
    }
}
