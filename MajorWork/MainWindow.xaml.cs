using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MajorWork.Logic.Models;
using MajorWork.Logic.Services;
using MajorWork.ViewModels;

namespace MajorWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            generateGrid();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        MazePlayService play;

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            //TODO Integrate into MazePlayService
            if (e.Key == Key.Up)
            {
                throw new NotImplementedException();
            }

            if (e.Key == Key.Down)
            {
                throw new NotImplementedException();
            }

            if (e.Key == Key.Left)
            {
                throw new NotImplementedException();
            }

            if (e.Key == Key.Right)
            {
                throw new NotImplementedException();
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            //TODO MAJOR REFACTORING
            maze ReferenceMaze = new maze();
           

            try
            {
                MazeGenerationService mazeGenService = new MazeGenerationService(ref ReferenceMaze, Convert.ToInt32(lengthTxt.Text), Convert.ToInt32(widthTxt.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#2196F3");
            blank.Background = brush;
            //Draw mazeGraphic = new Draw(mazeGenService, blank);
            //play = new MazePlayService(mazeGenService);
        }

        private void generateGrid()
        {
            //Enable for debugging
            blank.ShowGridLines = true;

            //Column Definitions
            for (int i = 0; i < 10; i++) //Change to user width
            {
                blank.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //Row Definitons
            for (int i = 0; i < 10; i++)
            {
                blank.RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}
