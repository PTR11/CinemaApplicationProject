using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class TicketSellViewModel : ViewModelBase
    {
        private string _movieFilter;
        private DateTime _dateFilter;
        private int _roomsNumber;

        public ObservableCollection<RoomViewModel> _rooms;

        public ObservableCollection<RoomViewModel> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; OnPropertyChanged(); }
        }

        public int RoomsNumber
        {
            get { return _roomsNumber; }
            set { _roomsNumber = value; OnPropertyChanged(); }
        }

        public String MovieFilter
        {
            get { return _movieFilter; }
            set { _movieFilter = value; OnPropertyChanged(); }
        }

        public DateTime DateFilter
        {
            get { return _dateFilter; }
            set { _dateFilter = value; OnPropertyChanged(); }
        }
    }
}
