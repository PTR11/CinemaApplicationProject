using CinemaApplicationProject.Desktop.Model.Services;
using CinemaApplicationProject.Desktop.View.Admin;
using CinemaApplicationProject.Desktop.View.Admin.Pages;
using CinemaApplicationProject.Desktop.Viewmodel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaApplicationProject.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AdminMainWindow _view;
        private MainViewModel _mainViewModel;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _mainViewModel = new MainViewModel();
            _mainViewModel.MovieDetailsVisible += MovieDetailsVisible;
            _view = new AdminMainWindow
            {
                DataContext = _mainViewModel
            };
            _view.Show();
        }

        private void MovieDetailsVisible(object sender, bool e)
        {
            _view.Menu.Visibility = e ? Visibility.Visible : Visibility.Hidden ;
        }
    }
}
