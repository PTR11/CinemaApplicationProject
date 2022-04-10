using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class PresenceViewModel : ViewModelBase
    {
        private double _dutyTime;
        private DateTime _login;
        private DateTime _logout;

        public PresenceViewModel()
        {
            this.DutyTime = 0;
        }

        public double DutyTime
        {
            get { return _dutyTime; }
            set { _dutyTime = value; OnPropertyChanged(); }
        }

        public DateTime Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }

        public DateTime Logout
        {
            get { return _logout; }
            set { _logout = value; OnPropertyChanged(); }
        }

        public static explicit operator PresenceViewModel(PresenceDTO dto) => new PresenceViewModel
        {
            Id = dto.Id,
            DutyTime = Math.Round(dto.DutyTime, 2),
            Login = dto.Login,
            Logout = dto.Logout,
        };

    }
}
