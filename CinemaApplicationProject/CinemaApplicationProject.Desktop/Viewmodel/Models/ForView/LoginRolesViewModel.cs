using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class LoginRolesViewModel : ViewModelBase
    {
        private String _name;
        public ICommand Command { get; set; }
        public LoginRolesViewModel()
        {
            _name = "";
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

    }
}
