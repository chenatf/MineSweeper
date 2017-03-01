using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Minesweep.Model;

namespace Minesweep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {

        #region Depedency Properties

        public int Mines
        {
            get { return (int)GetValue(MinesProperty); }
            private set { SetValue(MinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinesProperty =
            DependencyProperty.Register("Mines", typeof(int), typeof(MainWindow), new PropertyMetadata(0));


        #endregion

        #region Methods
        public void SetDifficultyBeginner()
        {

        }
        public void SetDifficultyIntermediate()
        {

        }
        public void SetDifficultyExpert()
        {

        }

        public void SetDifficultyCustom()
        {

        }

        public void SetDifficultyCustom(Difficulty difficulty)
        {

        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Details
        private void DoSetDifficulty(Difficulty difficulty)
        {

        }
        #endregion

        private void MenuSetDificulty(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if(menuItem != null)
            {
                if(menuItem == Menu_Game_Beginner)
                    SetDifficultyBeginner();
                if(menuItem == Menu_Game_Intermediate)
                    SetDifficultyIntermediate();
                if(menuItem == Menu_Game_Expert)
                    SetDifficultyExpert();
                if(menuItem == Menu_Game_Custom)
                    SetDifficultyCustom();
            }
        }
    }
}
