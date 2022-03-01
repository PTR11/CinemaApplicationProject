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
        public ObservableCollection<RoomViewModel> _rooms;

        public ObservableCollection<RoomViewModel> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; OnPropertyChanged(); }
        }
    }
}
