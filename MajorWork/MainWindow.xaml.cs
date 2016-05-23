﻿using System;
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

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        internal Grid myGrid { get; set; }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            //TODO Integrate into MazePlayService
            if (e.Key == Key.Up)
            {
                Label Message = new Label
                {
                    Content = "Fired!"
                };
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            MazeGenerationService maze = new MazeGenerationService(10, 10); //Switch to custom width
            Draw mazeGraphic = new Draw(maze, MazeGridUI);
           
            
        }


    }
}