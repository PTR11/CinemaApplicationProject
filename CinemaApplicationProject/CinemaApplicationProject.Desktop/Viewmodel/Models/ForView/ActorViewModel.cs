using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ActorViewModel : ViewModelBase
    {
        public int _id;
        public string _name;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public ActorViewModel ShallowClone()
        {
            return (ActorViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(ActorViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
        }

        public static explicit operator ActorViewModel(ActorsDTO dto) => new ActorViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
        };

        public static explicit operator ActorsDTO(ActorViewModel mvm) => new ActorsDTO
        {
            Id = mvm.Id,
            Name = mvm.Name,
        };
    }
}
