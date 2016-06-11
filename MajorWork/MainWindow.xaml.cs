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

using MajorWork.ViewModels;

namespace MajorWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindow;
        public Grid gridBlank;

        public MainWindow()
        {
            MainWindowViewModel MainWindow = new MainWindowViewModel();
            InitializeComponent();
            generateGrid();
            gridBlank = blank;

            _mainWindow = MainWindow;

            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);
        }

        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            _mainWindow.Play(blank, e);
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnGenerate.Content == "Generate")
            {
                _mainWindow.Generate(blank);
                blank.Visibility = Visibility.Visible;
                btnGenerate.Content = "Clear";
                return;

            }

            if ((string)btnGenerate.Content == "Clear")
            {
                _mainWindow.Clear();
                blank.Children.Clear();
                blank.Visibility = Visibility.Hidden;
                btnGenerate.Content = "Generate";
                return;
            }
        }

        private void generateGrid()
        {
            //Enable for debugging
            blank.ShowGridLines = false;

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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Open your web browser?", "Yes and yes", MessageBoxButton.YesNo);
            
            System.Diagnostics.Process.Start("http://google.com");
        }
    }
}
