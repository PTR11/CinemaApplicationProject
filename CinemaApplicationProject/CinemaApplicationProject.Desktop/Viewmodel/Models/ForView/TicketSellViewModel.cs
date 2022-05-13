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
        private MainViewModel _main;
        public int GuestId;

        private TicketViewModel _selectedTicket;
        private TicketsUsersViewModel _selectedUser;
        private ObservableCollection<RoomViewModel> _rooms;
        private ObservableCollection<TicketsUsersViewModel> _users;
        private ObservableCollection<TicketViewModel> _tickets;
        private ObservableCollection<TicketsCounterViewModel> _ticketsCounter;
        private List<RentViewModel> _rents;
        private ObservableCollection<Field> _field = new ObservableCollection<Field>();


        public List<RentViewModel> Rents
        {
            get { return _rents; }
            set { _rents = value; OnPropertyChanged(); }
        }

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

        public TicketsUsersViewModel SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TicketsUsersViewModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TicketsCounterViewModel> TicketsCounter
        {
            get { return _ticketsCounter; }
            set { _ticketsCounter = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TicketViewModel> Tickets
        {
            get { return _tickets; }
            set { _tickets = value; OnPropertyChanged(); }
        }

        public ObservableCollection<RoomViewModel> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Field> Field
        {
            get { return _field; }
            set { _field = value; OnPropertyChanged(); }
        }

        public MainViewModel Main
        {
            get { return _main; }
            set { _main = value; OnPropertyChanged(); }
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

        public DelegateCommand SelectTicketUser { get; set; }

        public DelegateCommand UnSelectTicketUser { get; set; }

        public TicketSellViewModel()
        {
            Field = new ObservableCollection<Field>();
            Rooms = new ObservableCollection<RoomViewModel>();
            Tickets = new ObservableCollection<TicketViewModel>();
            TicketsCounter = new ObservableCollection<TicketsCounterViewModel>();
            Places = new List<Place>();
            Rents = new List<RentViewModel>();
            Users = new ObservableCollection<TicketsUsersViewModel>();
            SelectTicketUser = new DelegateCommand(_ => ShowRent());
            UnSelectTicketUser = new DelegateCommand(_ => UnSelect());
        }

        public void CalculatePrice(int price)
        {
            this.Price += price;
        }

        public void AddToSummary(String type,int price)
        {
            var counter = this.TicketsCounter.FirstOrDefault(t => t.Type.Equals(type));
            counter.Count++;
            CalculatePrice(price);
        }

        public void RemoveFromSummary(Place place)
        {
            var counter = this.TicketsCounter.FirstOrDefault(t => t.Type.Equals(place.TicketCategory));
            counter.Count--;
            var price = this.Tickets.FirstOrDefault(ticket => ticket.Type.Equals(place.TicketCategory)).Price;
            CalculatePrice(-price);
        }


        public void AddPlace(int x, int y)
        {
            this.Places.Add(new Place
            {
                X = x,
                Y = y,
                TicketCategory = this.SelectedTicket.Type
            });
            AddToSummary(this.SelectedTicket.Type, this.SelectedTicket.Price);
        }

        public void RemovePlace(int x, int y)
        {
            var rent = this.Rents.FirstOrDefault(m => m.X == x && m.Y == y);
            var place = this.Places.FirstOrDefault(m => m.X == x && m.Y == y);
            this.Places.Remove(place);
            RemoveFromSummary(place);
        }

        public void PlaceManagement(int number)
        {
            Field act = this.Field[number];
            int x = act.X;
            int y = act.Y;
            var exist = this.Places.FirstOrDefault(p => p.X == x && p.Y == y);
            if (exist == null)
            {
                if (this.SelectedTicket != null)
                {
                    act.Background = "Green";
                    this.AddPlace(x, y);
                }
                else
                {
                    _main.MessageSender("Please select a ticket type");
                }
            }
            else
            {
                act.Background = "White";
                this.RemovePlace(x, y);
            }
        }

        public void UnSelect()
        {
            this.Places = new List<Place>();
            this.Price = 0;
            foreach (var ticket in TicketsCounter)
            {
                ticket.Count = 0;
            }
            foreach (var field in this.Field)
            {
                var found = this.Rents.FirstOrDefault(f => f.X == field.X && f.Y == field.Y);
                if (found != null)
                {
                    if (found.EmployeeId == null)
                    {
                        field.Background = "Orange";
                    }
                    else
                    {
                        field.Background = "Red";
                    }

                }
                else
                {
                    field.Background = "White";
                }
            }
        }

        public void ShowRent()
        {
            UnSelect();

            var rentsOfUser = this.Rents.Where(m => m.GuestId == this.SelectedUser.Id && m.EmployeeId == null);
            foreach (var rent in rentsOfUser)
            {
                Field act = this.Field.FirstOrDefault(f => f.X == rent.X && f.Y == rent.Y);
                act.Background = "Yellow";
                var found = this.Tickets.FirstOrDefault(t => t.Id == rent.TicketId);
                this.Places.Add(new Place
                {
                    X = rent.X,
                    Y = rent.Y,
                    TicketCategory = found.Type
                });
                AddToSummary(found.Type, found.Price);
            }
        }
    }
}
