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
        private int _soldTickets;
        private int _soldProducts;
        public ObservableCollection<StatsViewModel> _stats;
        public ObservableCollection<PresenceViewModel> _presence;
        public EmployeeViewModel()
        {
            _userName = "";
            _name = "";
            _password = "";
            _email = "";
            _address = "";
            _birthday = "";
            _soldTickets = 0;
            _stats = new ObservableCollection<StatsViewModel>();
            _presence = new ObservableCollection<PresenceViewModel>();
            DeleteUser = new DelegateCommand(async _ => await Delete());
        }

        public DelegateCommand DeleteUser { get; set; }

        public async Task Delete()
        {
            var main = (MainViewModel)this.MainModel;
            await main.DeleteEntity("api/Users", this.Id, main.LoadInit);
            main.UserVisibility(false);
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
        
        public int SoldTickets
        {
            get { return _soldTickets; }
            set { _soldTickets = value; OnPropertyChanged(); }
        }

        public int SoldProducts
        {
            get { return _soldProducts; }
            set { _soldProducts = value; OnPropertyChanged(); }
        }

        public ObservableCollection<StatsViewModel> Stats
        {
            get { return _stats; }
            set { _stats = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PresenceViewModel> Presence
        {
            get { return _presence; }
            set { _presence = value; OnPropertyChanged(); }
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
            SoldTickets = dto.SoldTickets,
            SoldProducts = dto.SoldProducts,
            Stats = new(dto.Stats.ToList().Select(x => (StatsViewModel)x)),
            Presence = new(dto.Presence.ToList().Select(x => (PresenceViewModel)x)),
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
            Stats = new(mvm.Stats.ToList().Select(x => (StatsDTO)x)),
            //Stats = mvmnew(mvm.Stats.ToList().Select(x => (StatsDTO)x))
        };
    }
}
