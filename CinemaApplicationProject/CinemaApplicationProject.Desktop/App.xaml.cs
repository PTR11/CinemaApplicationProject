using CinemaApplicationProject.Desktop.Model.Services;
using CinemaApplicationProject.Desktop.View;
using CinemaApplicationProject.Desktop.View.Admin;
using CinemaApplicationProject.Desktop.Viewmodel.EventArguments;
using CinemaApplicationProject.Desktop.Viewmodel.Models;
using CinemaApplicationProject.Desktop.Viewmodel.Models.ForView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        private TicketSellViewModel _ticketSellViewModel;
        private RolesPickerWindow _rolesPickerWindow;
        private TicketUserWindow _ticketUserWindow;
        private ProductSellingWindow _pr;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _mainViewModel = new MainViewModel();
            _mainViewModel.MovieDetailsVisible += MovieDetailsVisible;
            _mainViewModel.ShowDetailsVisible += ShowDetailsVisible;
            _mainViewModel.RoomDetailsVisible += RoomDetailsVisible;
            _mainViewModel.TicketDetailsVisible += TicketDetailsVisible;
            _mainViewModel.UserDetailsVisible += UserDetailsVisible;
            _mainViewModel.RoleDetailsVisible += RoleDetailsVisible;
            _mainViewModel.ProductDetailsVisible += ProductDetailsVisible;
            _mainViewModel.MessageApplication += MessageApplication;
            _mainViewModel.ImageChanger += ImageChangerForMovie;
            _mainViewModel.ImageChanger2 += ImageChangerForProduct;
            _mainViewModel.Displayer += DisplayerMethod;
            _view = new AdminMainWindow
            {
                DataContext = _mainViewModel
            };

            _loginViewModel = new LoginViewModel();
            _loginViewModel.MessageApplication += MessageApplication;
            _loginViewModel.LoginSucceeded += new EventHandler<int>(async(s,e) => await AfterLogin(s,e));
            _login = new LoginWindow
            {
                DataContext = _loginViewModel
            };
            _pr = new ProductSellingWindow
            {
                DataContext = _mainViewModel
            };
            _ticketUserWindow = new TicketUserWindow
            {
                DataContext = _mainViewModel
            };
            _mainViewModel.CreateProductsFieldAsync();
            _login.Show();

        }

        private async void DisplayerMethod(object sender, string e)
        {
            _rolesPickerWindow.Close();
            await _mainViewModel.LoadInit();
            switch (e)
            {
                case "administrator": _view.Show(); break;
                case "ticket seller": _ticketUserWindow.Show(); break;
                case "buffet seller": _pr.Show(); break;
                default: return;
            }
        }

        private async Task AfterLogin(object sender, int e)
        {
            _login.Close();
            _mainViewModel.UserId = e;
            await _mainViewModel.LoadUserRoles();
            if(_mainViewModel.UserRolesList.Count > 1)
            {
                _rolesPickerWindow = new RolesPickerWindow
                {
                    DataContext = _mainViewModel
                };
                _rolesPickerWindow.ShowDialog();
            }
            else
            {
                await _mainViewModel.LoadInit();
                switch (_mainViewModel.UserRolesList[0].Name)
                {
                    case "administrator": _view.Show(); break;
                    case "ticket seller": _ticketUserWindow.Show(); break;
                    case "buffet seller": _pr.Show(); break;
                    default: return;
                }

            }
        }


        private async void ImageChangerForMovie(object sender, bool e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                _mainViewModel.SelectedMovie.Image = await File.ReadAllBytesAsync(dialog.FileName);
                _mainViewModel.SelectedMovie.ImageForeground = "Transparent";
            }
        }

        private async void ImageChangerForProduct(object sender, bool e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                _mainViewModel.SelectedProduct.Image = await File.ReadAllBytesAsync(dialog.FileName);
                _mainViewModel.SelectedProduct.ImageForeground = "Transparent";
            }
        }


        private void MovieDetailsVisible(object sender, bool e)
        {
            _view.Menu.Visibility = e ? Visibility.Visible : Visibility.Hidden ;
        }
        private void ShowDetailsVisible(object sender, bool e)
        {
            _view.ShowMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
        }
        private void RoomDetailsVisible(object sender, bool e)
        {
            _view.RoomMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
        }
        private void TicketDetailsVisible(object sender, bool e)
        {
            _view.TicketMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
        }
        private void ProductDetailsVisible(object sender, bool e)
        {
            _view.ProductMenu.Visibility = e ? Visibility.Visible : Visibility.Hidden;
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
            MessageBox.Show(e.Message, "CinemaApplicationProject_Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
