using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using MajorWork.Logic.Models;
using MajorWork.Logic.Services;

namespace MajorWork.ViewModels
{
    class Draw
    {
        public Draw(MazeGenerationService mazeGrid)
        {
            DrawMaze(mazeGrid);
        }

        private void DrawMaze(MazeGenerationService mazeGrid)
        {
            mazeGrid.mazeGrid.mazeGrid.ForEach(delegate(mazepoints s)
            {
                if (s.isWall == true)
                    DrawPixel(s);
                // Return data to the view
            });
        }

        private Line DrawPixel(mazepoints s)
        {
            Line myLine = new Line();
            //Get colour from UI
            myLine.Stroke = System.Windows.Media.Brushes.Lavender;
            myLine.X1 = s.X; //Grid starting position
            myLine.X2 = s.X + 1; //Grid final position

            myLine.Y1 = s.Y; //Grid starting position
            myLine.Y2 = s.Y + 1; //Grid final position
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;

            return myLine;

        }
    }
}
