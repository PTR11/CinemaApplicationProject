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
    public class TicketSellViewModel : ViewModelBase
    {
        private string _movieFilter;
        private DateTime _dateFilter;
        private int _roomsNumber;
        private List<Place> _places;
        private int _price = 0;
        private ShowViewModel _show;

        private TicketViewModel _selectedTicket;
        private ObservableCollection<RoomViewModel> _rooms;
        private ObservableCollection<TicketsCounterViewModel> _ticketsCounter = new ObservableCollection<TicketsCounterViewModel>();

        

        public List<Place> Places
        {
            get { return _places; }
            set { _places = value; OnPropertyChanged(); }
        }
        public ShowViewModel Show
        {
            get { return _show; }
            set { _show = value; OnPropertyChanged(); }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        public TicketViewModel SelectedTicket
        {
            get { return _selectedTicket; }
            set { _selectedTicket = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TicketsCounterViewModel> TicketsCounter
        {
            get { return _ticketsCounter; }
            set { _ticketsCounter = value; OnPropertyChanged(); }
        }

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

        public void CalculatePrice()
        {
            this.Price += this.SelectedTicket.Price;
        }

        public void AddToSummary()
        {
            var counter = this.TicketsCounter.FirstOrDefault(t => t.Type.Equals(this.SelectedTicket.Type));
            counter.Count++;
            CalculatePrice();
        }


        public void AddPlace(int x, int y)
        {
            this.Places.Add(new Place
            {
                X = x,
                Y = y,
                TicketCategory = this.SelectedTicket.Type
            });
            AddToSummary();
        }
    }
}
