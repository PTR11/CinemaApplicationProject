using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public  class EmployeeViewModel : ViewModelBase
    {
        private String _userName;
        private String _name;
        private String _password;
        private String _email;
        public String _address;
        public String _birthday;
        public ObservableCollection<StatsViewModel> _stats;


        public EmployeeViewModel()
        {
            _userName = "";
            _name = "";
            _password = "";
            _email = "";
            _address = "";
            _birthday = "";
            _stats = new ObservableCollection<StatsViewModel>();
        }
        public String UserName {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public String Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        public String Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        public String Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public String Birthday
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(); }
        }

        public ObservableCollection<StatsViewModel> Stats
        {
            get { return _stats; }
            set { _stats = value; OnPropertyChanged(); }
        }

        public MovieViewModel ShallowClone()
        {
            return (MovieViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(EmployeeViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
            UserName = rhs.UserName;
            Address = rhs.Address;
            Birthday = rhs.Birthday;
            Password = rhs.Password;
            Email = rhs.Email;
            Stats = rhs.Stats;
        }

        public static explicit operator EmployeeViewModel(EmployeesDTO dto) => new EmployeeViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            UserName = dto.UserName,
            Address = dto.Address,
            Birthday = dto.Birthday,
            Password = dto.Password,
            Email = dto.Email,
            Stats = new(dto.Stats.ToList().Select(x => (StatsViewModel)x))
        };

        public static explicit operator EmployeesDTO(EmployeeViewModel mvm) => new EmployeesDTO
        {
            Id = mvm.Id,
            Name = mvm.Name,
            UserName = mvm.UserName,
            Address = mvm.Address,
            Birthday = mvm.Birthday,
            Password = mvm.Password,
            Email = mvm.Email,
            //Stats = mvmnew(mvm.Stats.ToList().Select(x => (StatsDTO)x))
        };
    }
}
