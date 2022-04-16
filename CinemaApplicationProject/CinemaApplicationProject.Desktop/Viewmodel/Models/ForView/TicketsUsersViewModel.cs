using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class TicketsUsersViewModel : ViewModelBase
    {
        private String _userName;
        private String _email;


        public String UserName {

            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        public String Email {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        public static explicit operator TicketsUsersViewModel(GuestVDTO dto) => new TicketsUsersViewModel
        {
            Id = dto.Id,
            UserName = dto.UserName,
            Email = dto.Email,
        };


    }
}
