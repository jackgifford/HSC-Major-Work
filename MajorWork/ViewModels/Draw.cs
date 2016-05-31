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
        MainWindow drawWindow;
        public Draw(MazeGenerationService mazeGrid, Grid blank)
        {
            DrawMaze(mazeGrid, blank);
            drawWindow = new MainWindow();
        }

        private void DrawMaze(MazeGenerationService mazeGrid, Grid grid)
        {
            mazeGrid.mazeGrid.mazeGrid.ForEach(delegate(mazepoints s)
            {
                if (s.isWall == true)
                {
                    Rectangle myRect = new Rectangle();
                    myRect = DrawRect(s, myRect);
                    grid.Children.Add(myRect);
                    Grid.SetRow(myRect, s.Y);
                    Grid.SetColumn(myRect, s.X);
                }
                // Return data to the view
            });
        }

        private void readyPlayer(MazeGenerationService mazeGrid, Grid grid)
        {
            throw new NotImplementedException();
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
