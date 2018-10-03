using MineSweep.Model;
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

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game Game => (Game)DataContext;
        public MainWindow()
        {
            InitializeComponent();
            Game.Initialize(10, 10, 10);
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            Game.Initialize(10, 10, 10);
        }

        private void Unexplored_Click(object sender, RoutedEventArgs e)
        {
            var x = Grid.GetRow((UIElement)sender);
            var y = Grid.GetColumn((UIElement)sender);
            Game.Explore(x, y);
        }
    }
}
