using CinemaApplicationProject.Desktop.Model.Services;
using CinemaApplicationProject.Desktop.View;
using CinemaApplicationProject.Desktop.View.Admin;
using CinemaApplicationProject.Desktop.View.Admin.Pages;
using CinemaApplicationProject.Desktop.Viewmodel.EventArguments;
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
        private LoginWindow _login;
        private MainViewModel _mainViewModel;
        private LoginViewModel _loginViewModel;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _mainViewModel = new MainViewModel();
            _mainViewModel.MovieDetailsVisible += MovieDetailsVisible;
            _mainViewModel.RoomDetailsVisible += RoomDetailsVisible;
            _mainViewModel.TicketDetailsVisible += TicketDetailsVisible;
            _mainViewModel.UserDetailsVisible += UserDetailsVisible;
            _mainViewModel.RoleDetailsVisible += RoleDetailsVisible;
            _mainViewModel.MessageApplication += MessageApplication;
            _view = new AdminMainWindow
            {
                DataContext = _mainViewModel
            };
            _loginViewModel = new LoginViewModel();
            _loginViewModel.MessageApplication += MessageApplication;
            _loginViewModel.LoginSucceeded += AfterLogin;
            _login = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _login.Show();
        }

        private void AfterLogin(object sender, int e)
        {
            _login.Close();
            _mainViewModel.UserId = e;
            _view.Show();
        }

        private void MovieDetailsVisible(object sender, bool e)
        {
            _view.Menu.Visibility = e ? Visibility.Visible : Visibility.Hidden ;
        }
        private void RoomDetailsVisible(object sender, bool e)
        {
            _view.RoomMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
        }
        private void TicketDetailsVisible(object sender, bool e)
        {
            _view.TicketMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
        }

        private void UserDetailsVisible(object sender, bool e)
        {
            
            _view.UserMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
        }

        private void RoleDetailsVisible(object sender, bool e)
        {
            _view.RoleMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
        }

        private void MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "CinemaHW", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
