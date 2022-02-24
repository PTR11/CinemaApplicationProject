using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class RoomViewModel : ViewModelBase
    {
        public string _name;
        public int _width;
        public int _height;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged(); }
        }

        public int Heigth
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged(); }
        }

        public RoomViewModel ShallowClone()
        {
            return (RoomViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(RoomViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
            Width = rhs.Width;
            Heigth = rhs.Heigth;
        }

        public static explicit operator RoomViewModel(RoomsDTO dto) => new RoomViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Width = dto.Width,
            Heigth = dto.Heigth
        };

        public static explicit operator RoomsDTO(RoomViewModel vm) => new RoomsDTO
        {
            Id = vm.Id,
            Name = vm.Name,
            Width = vm.Width,
            Heigth = vm.Heigth
        };




    }
}
