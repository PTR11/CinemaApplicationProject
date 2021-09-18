using CinemaApplicationProject.Desktop.View.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace CinemaApplicationProject.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            Debug.WriteLine(((ListViewItem)((ListView)sender).SelectedItem).Name);
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "MainPage":
                    
                    usc = new MainPage();
                    usc.DataContext = this.DataContext;
                    GridMain.Children.Add(usc);
                    break;
                case "ProfilesPage":
                    usc = new ProfilesPage();
                    usc.DataContext = this.DataContext;
                    GridMain.Children.Add(usc);
                    break;
                case "ShowsPage":
                    usc = new ShowsPage();
                    usc.DataContext = this.DataContext;
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
    }
}
