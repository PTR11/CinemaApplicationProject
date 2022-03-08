using CinemaApplicationProject.Desktop.Model.Errors;
using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models
{
    public  class LoginViewModel : ViewModelBase
    {

        public Boolean IsLoading { get; set; }

        public String UserName { get; set; }

        public DelegateCommand LoginCommand { get; set; }

        public event EventHandler<int> LoginSucceeded;

        public LoginViewModel()
        {
            IsLoading = false;
            LoginCommand = new DelegateCommand(_ => !IsLoading, param => LoginAsync(param as PasswordBox));
        }

        private async void LoginAsync(PasswordBox passwordBox)
        {
            try
            {
                IsLoading = true;
                var result = await _service.LoginAsync(UserName, passwordBox.Password);
                if (result != 0)
                    LoginSucceeded?.Invoke(this, result);
                else
                {
                    IsLoading = false;
                    OnMessageApplication("Login failed");
                }
                    
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
