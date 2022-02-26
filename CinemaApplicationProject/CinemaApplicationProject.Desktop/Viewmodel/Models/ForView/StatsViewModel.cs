using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class StatsViewModel : ViewModelBase
    {
        private String _name;
        private int _salary;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public int Salary
        {
            get { return _salary; }
            set { _salary = value; OnPropertyChanged(); }
        }

        public StatsViewModel ShallowClone()
        {
            return (StatsViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(StatsViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
            Salary = rhs.Salary;
        }

        public static explicit operator StatsViewModel(StatsDTO dto) => new StatsViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Salary = dto.Salary,
        };

        public static explicit operator StatsDTO(StatsViewModel mvm) => new StatsDTO
        {
            Id = mvm.Id,
            Name = mvm.Name,
            Salary = mvm.Salary
        };
    }
}
