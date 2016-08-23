using System;
using System.ComponentModel;
using System.Configuration;
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
        private byte _red;
        private byte _green;
        private byte _blue;

        public MainWindow()
        {
            var mainWindow = new MainWindowViewModel();
            InitializeComponent();
            _mainWindow = mainWindow;

            var backgroundWorker = new BackgroundWorker();
            _worker = backgroundWorker;
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            _worker.WorkerReportsProgress = true;
            _mainWindow.OnProgressUpdate += _mainWindow_OnProgressUpdate; //Invoke _mainWindow_OnProgressUpdate whenever the OnProgressUpdate event is called in _mainWindow
            PreviewKeyDown += MainWindow_PreviewKeyDown;

            RunTut();
        }

        void _mainWindow_OnProgressUpdate(int value) //Return to ui thread
        {
            Dispatcher.Invoke(delegate
            {
                LoadingBar.Value += value;
            });

        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _mainWindow.Generate(blank, _userLength);
            //Run all background tasks here
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _mainWindow.DrawGrid(blank, _red, _green, _blue);

            blank.Visibility = Visibility.Visible;
            LoadingBar.Visibility = Visibility.Collapsed;
            LoadingScreen.Visibility = Visibility.Collapsed;
            BtnGenerate.Content = "Clear";
            BtnGenerate.IsEnabled = true;
            //Update UI once worker completed work
        }


        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up | e.Key == Key.Left | e.Key == Key.Right | e.Key == Key.Down)
            {
                e.Handled = true;
                if (_mainWindow.Play(e))
                {
                    var message = MessageBox.Show("Game won!");
                }

            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _userLength = Convert.ToInt32(LengthTxt.Text);
                switch ((string)BtnGenerate.Content)
                {
                    case "Generate":
                        LoadingBar.Visibility = Visibility.Visible;
                        LoadingScreen.Visibility = Visibility.Visible;
                        BtnSolve.IsEnabled = true;
                        BtnGenerate.IsEnabled = false;
                        _red = (byte) SliderRed.Value;
                        _blue = (byte) SliderBlue.Value;
                        _green = (byte) SliderGreen.Value;

                        CallBackGroundWorker();
                        GenerateGrid();
                        
                        break;

                    case "Clear":
                        _mainWindow.Clear();
                        blank.Children.Clear();
                        blank.Visibility = Visibility.Hidden;
                        BtnGenerate.Content = "Generate";
                        BtnSolve.IsEnabled = false;
                        blank.ColumnDefinitions.Clear();
                        blank.RowDefinitions.Clear();
                        break;
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Please insert a number");
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
                blank.ColumnDefinitions.Add(new ColumnDefinition());


            //Row Definitons
            for (int i = 0; i < _userLength; i++)
                blank.RowDefinitions.Add(new RowDefinition());
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            var value = MessageBox.Show("Open your web browser?", "Maze Generator", MessageBoxButton.YesNo);
            if (value == MessageBoxResult.Yes)
                System.Diagnostics.Process.Start("http://shmacktus.github.io/HSC-Major-Work");
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Solve();
        }

        private void RunTut() //Opens the tutorial the first time the application runs on the system
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["Tutorial"].Value == "True")
            {
                //Launch message box
                var value =
                    MessageBox.Show(
                        "Hi! Since this is your first time running the application would you like to open the tutorial in your web browser?",
                        "Maze Generator: Tutorial", MessageBoxButton.YesNo);

                if (value == MessageBoxResult.Yes)
                    System.Diagnostics.Process.Start("http://shmacktus.github.io/HSC-Major-Work");

                config.AppSettings.Settings["Tutorial"].Value = "False";
                config.Save(ConfigurationSaveMode.Modified);
            }


        }
    }
}
