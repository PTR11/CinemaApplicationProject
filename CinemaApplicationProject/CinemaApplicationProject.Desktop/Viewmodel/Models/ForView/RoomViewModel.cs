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
    public class RoomViewModel : ViewModelBase
    {
        public string _name;
        public int _width;
        public int _height;
        private ObservableCollection<ShowViewModel> _shows;
        private List<ShowViewModel> _tmpShows;

        public ObservableCollection<ShowViewModel> Shows
        {
            get { return _shows; }
            set { _shows = value; OnPropertyChanged(); }
        }

        public List<ShowViewModel> TmpShows
        {
            get { return _tmpShows; }
            set { _tmpShows = value; }
        }
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

        private static List<ShowViewModel> ConvertShowsToVM(ICollection<ShowsDTO> m) => new(m.ToList().Select(x => (ShowViewModel) x));

        public static explicit operator RoomViewModel(RoomsDTO dto) => new RoomViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Width = dto.Width,
            Heigth = dto.Heigth,
            Shows = dto.Shows != null ? new ObservableCollection<ShowViewModel>(ConvertShowsToVM(dto.Shows)) : null,
            TmpShows = dto.Shows != null ? ConvertShowsToVM(dto.Shows) : null
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
