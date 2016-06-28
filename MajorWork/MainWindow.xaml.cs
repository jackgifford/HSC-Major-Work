using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


using MajorWork.ViewModels;

namespace MajorWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainWindowViewModel _mainWindow;
        private int _userLength;
        private readonly BackgroundWorker _worker;

        public MainWindow()
        {
            MainWindowViewModel mainWindow = new MainWindowViewModel();
            InitializeComponent();
          

            _mainWindow = mainWindow;

            var backgroundWorker = new BackgroundWorker();
            _worker = backgroundWorker;
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            _worker.ProgressChanged += _worker_ProgressChanged;
            _worker.WorkerReportsProgress = true;
            

            PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.LoadingBar.Value += 100;
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _mainWindow.Generate(blank, _userLength);
            //Run all background tasks here
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _mainWindow.DrawGrid(blank, _userLength);
            blank.Visibility = Visibility.Visible;
            btnGenerate.Content = "Clear";
            //Update UI once worker completed work
        }


        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up | e.Key == Key.Left | e.Key == Key.Right | e.Key == Key.Down)
            {
                e.Handled = true;
                _mainWindow.Play(blank, e);
            }
         
        }

      
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _userLength = Convert.ToInt32(lengthTxt.Text);
                switch ((string) btnGenerate.Content)
                {
                    case "Generate":

                        CallBackGroundWorker();
                        GenerateGrid();
                        return;
                    case "Clear":
                        _mainWindow.Clear();
                        blank.Children.Clear();
                        blank.Visibility = Visibility.Hidden;
                        btnGenerate.Content = "Generate";
                        break;
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Incorrect Input");
            }
        }

        private void CallBackGroundWorker()
        {
            _worker.RunWorkerAsync();
        }

        private void GenerateGrid()
        {
            //Enable for debugging
            blank.ShowGridLines = false;

            //Column Definitions
            for (int i = 0; i < _userLength; i++) //Change to user width
            {
                blank.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //Row Definitons
            for (int i = 0; i < _userLength; i++)
            {
                blank.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open your web browser?", "Yes and yes", MessageBoxButton.YesNo);
            
            System.Diagnostics.Process.Start("http://google.com");
        }
    }
}
