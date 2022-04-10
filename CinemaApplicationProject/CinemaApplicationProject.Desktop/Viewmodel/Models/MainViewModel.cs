using CinemaApplicationProject.Desktop.Model.Errors;
using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Desktop.Viewmodel.Models.ForView;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<MovieViewModel> _movies = new ObservableCollection<MovieViewModel>();

        private ObservableCollection<MovieViewModel> _moviesStat = new ObservableCollection<MovieViewModel>();

        private ObservableCollection<ProductStatViewModel> _productsStat = new ObservableCollection<ProductStatViewModel>();

        private ObservableCollection<ShowViewModel> _shows = new ObservableCollection<ShowViewModel>();

        private ObservableCollection<ActorViewModel> _actors = new ObservableCollection<ActorViewModel>();

        private ObservableCollection<RoomViewModel> _rooms = new ObservableCollection<RoomViewModel>();

        private ObservableCollection<TicketViewModel> _tickets = new ObservableCollection<TicketViewModel>();

        private ObservableCollection<EmployeeViewModel> _users = new ObservableCollection<EmployeeViewModel>();

        private ObservableCollection<StatsViewModel> _roles = new ObservableCollection<StatsViewModel>();

        private ObservableCollection<ProductViewModel> _warehouse = new ObservableCollection<ProductViewModel>();



        private ObservableCollection<String> _categoriesList = new ObservableCollection<String>();
        private ObservableCollection<String> _actorsList = new ObservableCollection<String>();
        private ObservableCollection<String> _rolesList = new ObservableCollection<String>();
        private ObservableCollection<String> _moviesList = new ObservableCollection<String>();
        private ObservableCollection<String> _roomsList = new ObservableCollection<String>();

        private List<ActorViewModel> _allActors = new List<ActorViewModel>();

        private List<CategoryViewModel> _allCategories = new List<CategoryViewModel>();

        private List<StatsViewModel> _allRoles = new List<StatsViewModel>();

        private ShowWeekViewModel _week = new ShowWeekViewModel();

        public bool _addMovie = false;

        private bool _addRoom = false;

        private bool _addTicket = false;

        private bool _addShow = false;

        private bool _addUser = false;

        private bool _addRole = false;

        public bool _addProduct = false;

        private int _userId = 0;

        private Int32 _gridW;
        private Int32 _gridH;

        public Int32 GridW
        {
            get { return _gridW; }
            set
            {
                if (_gridW != value)
                {
                    _gridW = value;
                    OnPropertyChanged();
                }
            }
        }

        public Int32 GridH
        {
            get { return _gridH; }
            set
            {
                if (_gridH != value)
                {
                    _gridH = value;
                    OnPropertyChanged();
                }
            }
        }

        private TicketSellViewModel _ticketsell;

        private ProductSellViewModel _productsell;

        private MovieViewModel _selectedMovie;

        private EmployeeViewModel _selectedUser;

        private ShowViewModel _selectedShow;

        private ShowViewModel _selectedTicketShow;

        private RoomViewModel _selectedRoom;

        private TicketViewModel _selectedTicket;

        private StatsViewModel _selectedRole;

        private ProductViewModel _selectedProduct;

        private ProductStatViewModel _selectedProductStat;

        private ActorViewModel _newActor;

        private CategoryViewModel _newCategory;

        private StatsViewModel _newRole;

        public List<ShowViewModel> _list;

        public String name = "faszom";

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public ObservableCollection<ProductViewModel> Warehouse
        {
            get { return _warehouse; }
            set { _warehouse = value; OnPropertyChanged(); }
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

        public ObservableCollection<MovieViewModel> Movies
        {
            get { return _movies; }
            set { _movies = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MovieViewModel> MoviesStat
        {
            get { return _moviesStat; }
            set { _moviesStat = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ProductStatViewModel> ProductsStat
        {
            get { return _productsStat; }
            set { _productsStat = value; OnPropertyChanged(); }
        }

        public ObservableCollection<EmployeeViewModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShowViewModel> Shows
        {
            get { return _shows; }
            set { _shows = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ActorViewModel> Actors
        {
            get { return _actors; }
            set { _actors = value; OnPropertyChanged(); }
        }

        public ObservableCollection<StatsViewModel> Roles
        {
            get { return _roles; }
            set { _roles = value; OnPropertyChanged(); }
        }

        public ObservableCollection<String> ActorsList
        {
            get { return _actorsList; }
            set { _actorsList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<String> RolesList
        {
            get { return _rolesList; }
            set { _rolesList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<String> CategoriesList
        {
            get { return _categoriesList; }
            set { _categoriesList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<String> MoviesList
        {
            get { return _moviesList; }
            set { _moviesList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<String> RoomsList
        {
            get { return _roomsList; }
            set { _roomsList = value; OnPropertyChanged(); }
        }

        public ShowWeekViewModel Week
        {
            get { return _week; }
            set { _week = value; OnPropertyChanged(); }
        }

        public TicketSellViewModel TicketSell
        {
            get { return _ticketsell; }
            set { _ticketsell = value; OnPropertyChanged(); }
        }

        public ProductSellViewModel ProductSell
        {
            get { return _productsell; }
            set { _productsell = value; OnPropertyChanged(); }
        }

        public MovieViewModel SelectedMovie
        {
            get { return _selectedMovie; }
            set { _selectedMovie = value; OnPropertyChanged(); }
        }

        public ShowViewModel SelectedShow
        {
            get { return _selectedShow; }
            set { _selectedShow = value; OnPropertyChanged(); }
        }

        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public ProductStatViewModel SelectedProductStat
        {
            get { return _selectedProductStat; }
            set { _selectedProductStat = value; OnPropertyChanged(); }
        }

        public ShowViewModel SelectedTicketShow
        {
            get { return _selectedTicketShow; }
            set { _selectedTicketShow = value; OnPropertyChanged(); }
        }

        public RoomViewModel SelectedRoom
        {
            get { return _selectedRoom; }
            set { _selectedRoom = value; OnPropertyChanged(); }
        }

        public TicketViewModel SelectedTicket
        {
            get { return _selectedTicket; }
            set { _selectedTicket = value; OnPropertyChanged(); }
        }

        public EmployeeViewModel SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        public StatsViewModel SelectedRole
        {
            get { return _selectedRole; }
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        public ActorViewModel NewActor
        {
            get { return _newActor; }
            set { _newActor = value; OnPropertyChanged(); }
        }

        public CategoryViewModel NewCategory
        {
            get { return _newCategory; }
            set { _newCategory = value; OnPropertyChanged(); }
        }

        public StatsViewModel NewRole
        {
            get { return _newRole; }
            set { _newRole = value; OnPropertyChanged(); }
        }

        public event EventHandler<bool> MovieDetailsVisible;
        public event EventHandler<bool> ShowDetailsVisible;
        public event EventHandler<bool> RoomDetailsVisible;
        public event EventHandler<bool> TicketDetailsVisible;
        public event EventHandler<bool> UserDetailsVisible;
        public event EventHandler<bool> RoleDetailsVisible;
        public event EventHandler<bool> ProductDetailsVisible;
        public event EventHandler<bool> ImageChanger;
        public event EventHandler<bool> ImageChanger2;

        public DelegateCommand SelectMovie { get; set; }
        public DelegateCommand SelectMovieStat { get; set; }
        public DelegateCommand SelectRoom { get; set; }
        public DelegateCommand SelectTicket { get; set; }
        public DelegateCommand SelectUser { get; set; }
        public DelegateCommand SelectRole { get; set; }
        public DelegateCommand SelectProduct { get; set; }

        public DelegateCommand SelectProductStat { get; set; }

        public DelegateCommand TicketSearch { get; set; }
        public DelegateCommand AddNewActor { get; set; }
        public DelegateCommand AddNewCategory { get; set; }
        public DelegateCommand AddNewMovie { get; set; }
        public DelegateCommand AddNewShow { get; set; }
        public DelegateCommand AddNewRoom { get; set; }
        public DelegateCommand AddNewTicket { get; set; }
        public DelegateCommand AddNewUser { get; set; }
        public DelegateCommand AddNewRole { get; set; }
        public DelegateCommand AddNewRents { get; set; }
        public DelegateCommand AddNewProduct { get; set; }
        public DelegateCommand AddNewSellingProducts { get; set; }


        public DelegateCommand UpdateShow { get; set; }
        public DelegateCommand UpdateMovie { get; set; }
        public DelegateCommand UpdateRoom { get; set; }
        public DelegateCommand UpdateTicket { get; set; }
        public DelegateCommand UpdateUser { get; set; }
        public DelegateCommand UpdateRole { get; set; }
        public DelegateCommand UpdateProduct { get; set; }

        public DelegateCommand DeleteMovie { get; set; }

        public DelegateCommand ChangeImageCommand { get; set; }
        public DelegateCommand ChangeImageCommand2 { get; set; }



        public DelegateCommand LastWeek { get; set; }
        public DelegateCommand ActualWeek { get; set; }
        public DelegateCommand NextWeek { get; set; }


        public MainViewModel()
        {
            //Selectors
            bool tmp = true;
            SelectMovie = new DelegateCommand(_ => ShowDetails(SelectedMovie, ref _addMovie, MovieDetailsVisible));
            SelectMovieStat = new DelegateCommand(_ => ShowDetails(SelectedMovie, ref tmp, null));
            SelectRoom = new DelegateCommand(_ => ShowDetails(SelectedRoom, ref _addRoom, RoomDetailsVisible));
            SelectTicket = new DelegateCommand(_ => ShowDetails(SelectedTicket, ref _addTicket, TicketDetailsVisible));
            SelectUser = new DelegateCommand(_ => ShowDetails(SelectedUser, ref _addUser, UserDetailsVisible));
            SelectRole = new DelegateCommand(_ => ShowDetails(SelectedRole, ref _addRole, RoleDetailsVisible));
            SelectProduct = new DelegateCommand(_ => ShowDetails(SelectedProduct, ref _addProduct, ProductDetailsVisible));
            SelectProductStat = new DelegateCommand(_ => ShowDetails(SelectedProductStat, ref tmp, null));


            //Add news (in update menu)
            AddNewActor = new DelegateCommand(_ => !(NewActor is null), async _ => await AddActor());
            AddNewCategory = new DelegateCommand(_ => !(NewActor is null), async _ => await AddCategory());
            AddNewRole = new DelegateCommand(_ => !(NewRole is null), async _ => await AddRole());

            TicketSearch = new DelegateCommand(_ => FilterTicketRooms());

            //Add new entity completly
            AddNewMovie = new DelegateCommand(_ => Adder(SelectedMovie = new MovieViewModel(), ref _addMovie, MovieDetailsVisible));
            AddNewShow = new DelegateCommand(_ => Adder(SelectedShow = new ShowViewModel(), ref _addShow, ShowDetailsVisible));
            AddNewRoom = new DelegateCommand(_ => Adder(SelectedRoom = new RoomViewModel(), ref _addRoom, RoomDetailsVisible));
            AddNewTicket = new DelegateCommand(_ => Adder(SelectedTicket = new TicketViewModel(), ref _addTicket, TicketDetailsVisible));
            AddNewUser = new DelegateCommand(_ => Adder(SelectedUser = new EmployeeViewModel(), ref _addUser, UserDetailsVisible));
            AddNewRents = new DelegateCommand(_ => !(TicketSell is null), async _ => await AddRent());
            AddNewProduct = new DelegateCommand(_ => Adder(SelectedProduct = new ProductViewModel(), ref _addProduct, ProductDetailsVisible));
            AddNewSellingProducts = new DelegateCommand(_ => !(ProductSell is null), async _ => await AddNewProducts());
            //Updates
            UpdateShow = new DelegateCommand(_ => !(SelectedShow is null), async _ => await UpdateSelectedItem(_addShow, AddCreatedShow, SelectedShow.Id != 0, SetupShowUpdate, "api/Shows", LoadShows, LoadRooms));
            UpdateMovie = new DelegateCommand(_ => !(SelectedMovie is null), async _ => await UpdateSelectedItem(_addMovie, AddCreatedMovie, SelectedMovie.Title != null, SetupMovieUpdate, "api/Movies", LoadMovies, LoadShows, LoadActors, LoadCategories));
            UpdateRoom = new DelegateCommand(_ => !(SelectedRoom is null), async _ => await UpdateSelectedItem(_addRoom, AddCreatedRoom, SelectedRoom.Id != 0, SetupRoomUpdate, "api/Rooms", LoadRooms, LoadShows));
            UpdateTicket = new DelegateCommand(_ => !(SelectedTicket is null), async _ => await UpdateSelectedItem(_addTicket, AddCreatedTicket, SelectedTicket.Id != 0, SetupTicketUpdate, "api/Tickets", null));
            UpdateUser = new DelegateCommand(_ => !(SelectedUser is null), async _ => await UpdateSelectedItem(_addUser, AddCreatedUser, SelectedUser.Id != 0, SetupUserUpdate, "api/Users", LoadUsers, LoadRoles));
            UpdateRole = new DelegateCommand(_ => !(SelectedRole is null), async _ => await UpdateSelectedItem(_addRole, AddCreatedTicket, SelectedRole.Id != 0, SetupRoleUpdate, "api/StatsAndPays", LoadRoles));
            UpdateProduct = new DelegateCommand(_ => !(SelectedProduct is null), async _ => await UpdateSelectedItem(_addProduct, AddCreatedProduct, SelectedProduct.Id != 0, SetupProductUpdate, "api/BuffetWarehouses", LoadProducts));

            ChangeImageCommand = new DelegateCommand(_ => ImageChangerMethod("movie"));
            ChangeImageCommand2 = new DelegateCommand(_ => ImageChangerMethod("product"));

            LastWeek = new DelegateCommand(_ => ChangeWeekToLast());
            ActualWeek = new DelegateCommand(async _ => await LoadShows());
            NextWeek = new DelegateCommand(_ => ChangeWeekToNext());
            
            NewActor = new ActorViewModel();
            NewActor.MainModel = this;
            NewCategory = new CategoryViewModel();
            NewRole = new StatsViewModel();

            SelectedShow = new ShowViewModel();
            SelectedShow.MainModel = this;
            SelectedMovie = new MovieViewModel();
            SelectedMovie.MainModel = this;
            SelectedRoom = new RoomViewModel();
            SelectedTicket = new TicketViewModel();
            SelectedUser = new EmployeeViewModel();
            SelectedRole = new StatsViewModel();
            SelectedTicketShow = new ShowViewModel();
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync(UserId);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void ImageChangerMethod(String caller)
        {
            if (caller.Equals("movie"))
            {
                ImageChanger?.Invoke(this, true);
            }
            else
            {
                ImageChanger2?.Invoke(this, true);
            }
        }

        public void InvokeShowDetails()
        {
            ShowDetailsVisible?.Invoke(this, true);
        }

        public async Task CreateMoviesFieldAsync()
        {
            TicketSell = new TicketSellViewModel();
            await LoadRents(SelectedTicketShow.Id);
            await LoadRentsUsers(SelectedTicketShow.Id);
            TicketSell.Show = SelectedTicketShow;
            TicketSell.Main = this;
            TicketSell.Tickets = Tickets;
            foreach (var tickets in Tickets)
            {
                TicketSell.TicketsCounter.Add(new TicketsCounterViewModel
                {
                    Type = tickets.Type,
                    Count = 0
                });
            }
            int width = Rooms.FirstOrDefault(r => r.Id == SelectedTicketShow.RoomId).Width;
            int height = Rooms.FirstOrDefault(r => r.Id == SelectedTicketShow.RoomId).Heigth;
            GridH = height;
            GridW = width;
            for (Int32 i = 0; i < height; i++)
            {
                for (Int32 j = 0; j < width; j++)
                {

                    TicketSell.Field.Add(new Field
                    {
                        Image = TicketSell.Rents.Contains(new RentViewModel { X = i, Y = j }) ? "/View/Images/reservedPlace.png" : "/View/Images/emptyPlace.png",
                        Background = Background(i, j),
                        X = i,
                        Y = j,
                        Number = i * width + j,
                        OnClick = new DelegateCommand(param => TicketSell.PlaceManagement(Convert.ToInt32(param))),
                    });
                }
            }
            width = Warehouse.Count >= 3 ? 3 : Warehouse.Count % 3;
        }

        public async Task CreateProductsFieldAsync()
        {
            ProductSell = new ProductSellViewModel();
            ProductSell.MainModel = this;
            await LoadProducts();
            ProductSell.Rows = Warehouse.Count % 3 == 0 ? Warehouse.Count / 3 : (Warehouse.Count / 3 )+ 1;
            for (Int32 i = 0; i < Warehouse.Count; i++)
            {
                ProductSell.Field.Add(new Field
                {
                    Id = Warehouse[i].Id,
                    ImageInBytes = Warehouse[i].Image,
                    Text = Warehouse[i].Name,
                    Price = Warehouse[i].Price,
                    //Background = Background(i, 0),
                    X = i / 3,
                    Y = i % 3,
                    Number = i,
                    OnClick = new DelegateCommand(param => ProductSell.PlaceManagement(Convert.ToInt32(param))),
                }) ;
            }
            int width = Warehouse.Count >= 3 ? 3 : Warehouse.Count % 3;
        }

        private String Background(int i, int j)
        {
            var element = TicketSell.Rents.FirstOrDefault(r => r.X == i && r.Y == j);
            if (element != null)
            {
                if (element.EmployeeId != null)
                {
                    return "Red";
                }
                else if (element.GuestId != null)
                {
                    return "Orange";
                }
            }
            return "White";

        }

        private void FilterTicketRooms()
        {
            var rooms = new List<RoomViewModel>(TicketSell.Rooms.ToList());
            var movie = TicketSell.MovieFilter;
            var date = TicketSell.DateFilter;
            if (movie != null || date != DateTime.Now.Date)
            {
                foreach (var room in rooms)
                {
                    List<ShowViewModel> showsForMovieFilter = new List<ShowViewModel>();
                    List<ShowViewModel> showsForDateFilter = new List<ShowViewModel>();
                    foreach (var show in room.TmpShows)
                    {
                        if (movie != null && show.MovieTitle.Equals(movie))
                        {
                            showsForMovieFilter.Add(show);
                        }
                        if (date != DateTime.Now.Date && show.Date.Date == date.Date)
                        {
                            showsForDateFilter.Add(show);
                        }
                    }
                    List<ShowViewModel> intersect;
                    if (movie != null && !movie.Equals("") && (date.Date.Year == 1 && date.Date.Month == 1 && date.Date.Day == 1))
                    {
                        intersect = new List<ShowViewModel>(showsForMovieFilter);
                    }
                    else if((movie == null || movie.Equals("")) && (date.Date.Year != 1))
                    {
                        intersect = new List<ShowViewModel>(showsForDateFilter);
                    }
                    else 
                    {
                        intersect = new List<ShowViewModel>(showsForMovieFilter.AsQueryable().Intersect(showsForDateFilter));
                    }
                    room.Shows = new ObservableCollection<ShowViewModel>(intersect);
                }
            }

        }

        private void ChangeWeekToNext()
        {
            DateTime mondayOfNextWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).AddDays(7);
            DateTime lastDayOfWeek = mondayOfNextWeek.AddDays(6);
            List<ShowViewModel> tmpShow = new List<ShowViewModel>(Shows.Where(m => mondayOfNextWeek.Ticks <= m.Date.Ticks && m.Date.Ticks <= lastDayOfWeek.Ticks).ToList());
            SeparateShowsByDay(tmpShow);
        }

        private void ChangeWeekToLast()
        {
            DateTime mondayOfLastWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).AddDays(-7);
            DateTime lastDayOfWeek = mondayOfLastWeek.AddDays(6);
            List<ShowViewModel> tmpShow = new List<ShowViewModel>(Shows.Where(m => mondayOfLastWeek.Ticks <= m.Date.Ticks && m.Date.Ticks <= lastDayOfWeek.Ticks).ToList());
            SeparateShowsByDay(tmpShow);

        }

        private bool IsOnThisWeek(DateTime date)    
        {
            DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6);
            DateTime endOfWeek = startOfWeek.AddDays(6).Date;
            return startOfWeek.Date.Ticks <= date.Date.Ticks && date.Date.Ticks <= endOfWeek.Date.Ticks;
        }

        private void SeparateShowsByDay(List<ShowViewModel> tmpShows)
        {
            Week = new ShowWeekViewModel();
            foreach (var show in tmpShows)
            {
                var movie = Movies.FirstOrDefault(x => x.Id == show.MovieId);
                show.DateFormat = show.Date.ToString("HH:mm") + " - " + show.Date.AddMinutes(movie.Length).ToString("HH:mm");
                switch (show.Date.DayOfWeek.ToString())
                {
                    case "Monday":
                        Week.Monday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek).OrderBy(m => m.Date.TimeOfDay));
                        Week.Monday.OrderBy(s => s.Date);
                        break;
                    case "Tuesday":
                        Week.Tuesday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek).OrderBy(m => m.Date.TimeOfDay));
                        break;
                    case "Wednesday":
                        Week.Wednesday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek).OrderBy(m => m.Date.TimeOfDay));
                        break;
                    case "Thursday":
                        Week.Thursday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek).OrderBy(m => m.Date.TimeOfDay));
                        break;
                    case "Friday":
                        Week.Friday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek).OrderBy(m => m.Date.TimeOfDay));
                        break;
                    case "Saturday":
                        Week.Saturday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek).OrderBy(m => m.Date.TimeOfDay));
                        break;
                    case "Sunday":
                        Week.Sunday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek).OrderBy(m => m.Date.TimeOfDay));
                        break;
                }
            }
        }

        private void AddColorsToMovies(ref List<RoomViewModel> tmpRooms)
        {
            Dictionary<string, string> movieTitles = new Dictionary<string, string>();
            foreach (var room in tmpRooms)
            {
                var shows = room.TmpShows;
                foreach (var show in shows)
                {
                    if (movieTitles.ContainsKey(show.MovieTitle))
                    {
                        show.Background = movieTitles.GetValueOrDefault(show.MovieTitle).Split("/")[0];
                        show.Foreground = movieTitles.GetValueOrDefault(show.MovieTitle).Split("/")[1];
                    }
                    else
                    {
                        Random rnd = new Random();
                        int nThreshold = 100;
                        Color randomColor = Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
                        int bgDelta = Convert.ToInt32((randomColor.R * 0.299) + (randomColor.G * 0.587) + (randomColor.B * 0.114));
                        Color foreColor = (255 - bgDelta > nThreshold) ? Color.FromRgb(255, 255, 255) : Color.FromRgb(0, 0, 0);
                        show.Background = randomColor.ToString();
                        show.Foreground = foreColor.ToString();
                        movieTitles.Add(show.MovieTitle, show.Background + "/" + show.Foreground);
                    }
                }
                room.Shows = new ObservableCollection<ShowViewModel>(room.TmpShows);
            }
        }
        public void MessageSender(String message)
        {
            OnMessageApplication(message);
        }

        #region DetailsVisibility

        public void MovieVisibility(bool visibility)
        {
            MovieDetailsVisible?.Invoke(this, visibility);
            if (!visibility)
            {
                SelectedMovie = new MovieViewModel();
                SelectedMovie.MainModel = this;
            }
        }
        public void ShowVisibility(bool visibility)
        {
            ShowDetailsVisible?.Invoke(this, visibility);
            if (!visibility)
            {
                SelectedShow = new ShowViewModel();
                SelectedShow.MainModel = this;
            }
        }

        public void UserVisibility(bool visibility)
        {
            UserDetailsVisible?.Invoke(this, visibility);
            if (!visibility)
            {
                SelectedUser = new EmployeeViewModel();
                SelectedUser.MainModel = this;
            }
        }

        public void RoleVisibility(bool visibility)
        {
            RoleDetailsVisible?.Invoke(this, visibility);
            if (!visibility)
            {
                SelectedRole = new StatsViewModel();
                SelectedRole.MainModel = this;
            }
        }

        public void ProductVisibility(bool visibility)
        {
            ProductDetailsVisible?.Invoke(this, visibility);
            if (!visibility)
            {
                SelectedProduct = new ProductViewModel();
                SelectedProduct.MainModel = this;
            }
        }
        #endregion

        #region Loaders

        public async Task LoadInit()
        {
            await LoadMovies();
            await LoadShows();
            await LoadActors();
            await LoadRooms();
            await LoadTickets();
            await LoadCategories();
            await LoadUsers();
            await LoadRoles();
            await LoadProducts();
        }

        private async Task LoadRentsUsers(int id)
        {
            List<TicketsUsersViewModel> tmpRentUsers = new List<TicketsUsersViewModel>();
            try
            {
                tmpRentUsers = new List<TicketsUsersViewModel>((await _service.LoadingAsync<GuestVDTO>("api/Rents/sellUsers/" + id))
                    .Select(rent => (TicketsUsersViewModel)rent));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            TicketSell.Users = new ObservableCollection<TicketsUsersViewModel>(tmpRentUsers);
        }


        private async Task LoadRents(int id)
        {
            List<RentViewModel> tmpRents = new List<RentViewModel>();
            try
            {
                tmpRents = new List<RentViewModel>((await _service.LoadingAsync<RentsDTO>("api/Rents/sell/" + id))
                    .Select(rent => (RentViewModel)rent));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            TicketSell.Rents = tmpRents;
        }


        private async Task LoadShows()
        {
            List<ShowViewModel> tmpShows = new List<ShowViewModel>();
            try
            {
                tmpShows = new List<ShowViewModel>((await _service.LoadingAsync<ShowsDTO>("api/Shows"))
                    .Select(movie => (ShowViewModel)movie));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }

            Shows = new ObservableCollection<ShowViewModel>(tmpShows);
            tmpShows = tmpShows.Where(m => IsOnThisWeek(m.Date)).ToList();
            SeparateShowsByDay(tmpShows);
        }
        private async Task LoadMovies()
        {
            List<MovieViewModel> tmpMovies = new List<MovieViewModel>();
            try
            {
                tmpMovies = new List<MovieViewModel>((await _service.LoadingAsync<MoviesDTO>("api/Movies"))
                    .Select(movie => (MovieViewModel)movie));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            List<MovieViewModel> tmpMovies2 = new List<MovieViewModel>(tmpMovies);
            tmpMovies2.AddRange(tmpMovies);
            Movies = new ObservableCollection<MovieViewModel>(tmpMovies2);
            foreach(var movie in Movies)
            {
                foreach(var actor in movie.Actors)
                {
                    actor.MainModel = this;
                }
                foreach (var cat in movie.Categories)
                {
                    cat.MainModel = this;
                }
            }
            MoviesList = new ObservableCollection<string>(tmpMovies.Select(m => m.Title).ToList());
        }

        public async Task LoadMoviesStat()
        {
            List<MovieViewModel> tmpMovies = new List<MovieViewModel>();
            try
            {
                tmpMovies = new List<MovieViewModel>((await _service.LoadingAsync<MoviesDTO>("api/Movies/statistics"))
                    .Select(movie => (MovieViewModel)movie));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            MoviesStat = new ObservableCollection<MovieViewModel>(tmpMovies.OrderBy(m => m.Average));
        }

        public async Task LoadProductsStat()
        {
            List<ProductStatViewModel> tmpProducts = new List<ProductStatViewModel>();
            try
            {
                tmpProducts = new List<ProductStatViewModel>((await _service.LoadingAsync<ProductStatDTO>("api/BuffetWarehouses/statistics"))
                    .Select(prod => (ProductStatViewModel)prod));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            ProductsStat = new ObservableCollection<ProductStatViewModel>(tmpProducts.OrderBy(m => m.AverageSell));
        }

        private async Task LoadProducts()
        {
            List<ProductViewModel> tmpProducts = new List<ProductViewModel>();
            try
            {
                tmpProducts = new List<ProductViewModel>((await _service.LoadingAsync<ProductDTO>("api/BuffetWarehouses"))
                    .Select(movie => (ProductViewModel)movie));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            Warehouse = new ObservableCollection<ProductViewModel>(tmpProducts);
            foreach (var product in Warehouse)
            {
                product.MainModel = this;
            }
        }

        private async Task LoadActors()
        {
            try
            {
                _allActors = new List<ActorViewModel>((await _service.LoadingAsync<ActorsDTO>("api/Actors"))
                    .Select(actor => (ActorViewModel)actor));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            foreach(var actor in _allActors)
            {
                actor.MainModel = this;
            }
            ActorsList = new ObservableCollection<string>(_allActors.Select(a => a._name).ToList());
        }
        private async Task LoadTickets()
        {
            List<TicketViewModel> tmpTickets = new List<TicketViewModel>();
            try
            {
                tmpTickets = new List<TicketViewModel>((await _service.LoadingAsync<TicketsDTO>("api/Tickets"))
                    .Select(ticket => (TicketViewModel)ticket));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            Tickets = new ObservableCollection<TicketViewModel>(tmpTickets);
        }
        private async Task LoadRooms()
        {
            List<RoomViewModel> tmpRooms = new List<RoomViewModel>();
            try
            {
                tmpRooms = new List<RoomViewModel>((await _service.LoadingAsync<RoomsDTO>("api/Rooms"))
                    .Select(movie => (RoomViewModel)movie));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            AddColorsToMovies(ref tmpRooms);
            Rooms = new ObservableCollection<RoomViewModel>(tmpRooms);
            RoomsList = new ObservableCollection<string>(tmpRooms.Select(r => "" + r.Name + " (" + r.Width + "x" + r.Heigth + ")").ToList());
            TicketSell = new TicketSellViewModel();
            TicketSell.Places = new List<Place>();
            TicketSell.Rooms = Rooms;
            TicketSell.RoomsNumber = Rooms.Count;
        }

        private async Task LoadCategories()
        {
            try
            {
                _allCategories = new List<CategoryViewModel>((await _service.LoadingAsync<CategoriesDTO>("api/Categories/all"))
                    .Select(category => (CategoryViewModel)category));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }

            foreach(var cat in _allCategories)
            {
                cat.MainModel = this;
            }
            CategoriesList = new ObservableCollection<string>(_allCategories.Select(a => a.Category).ToList());
        }
        private async Task LoadUsers()
        {
            List<EmployeeViewModel> tmpEmployees = new List<EmployeeViewModel>();
            try
            {
                tmpEmployees = new List<EmployeeViewModel>((await _service.LoadingAsync<EmployeesDTO>("api/Users"))
                    .Select(movie => (EmployeeViewModel)movie));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            Users = new ObservableCollection<EmployeeViewModel>(tmpEmployees);

            //MoviesList = new ObservableCollection<string>(tmpMovies.Select(m => m.Title).ToList());
        }
        private async Task LoadRoles()
        {
            try
            {
                _allRoles = new List<StatsViewModel>((await _service.LoadingAsync<StatsDTO>("api/StatsAndPays"))
                    .Select(ticket => (StatsViewModel)ticket));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            Roles = new ObservableCollection<StatsViewModel>(_allRoles);
            RolesList = new ObservableCollection<string>(_allRoles.Select(a => a.Name).ToList());
        }

        #endregion

        #region Setups

        private ShowsDTO SetupShowUpdate()
        {
            ShowViewModel updatetedShow = new ShowViewModel();
            updatetedShow.CopyFrom(SelectedShow);
            updatetedShow.RoomName = updatetedShow.RoomName.Split("(")[0].Trim();
            updatetedShow.MovieId = Movies.FirstOrDefault(m => m.Title.Equals(updatetedShow.MovieTitle)).Id;
            updatetedShow.RoomId = Rooms.FirstOrDefault(m => m.Name.Equals(updatetedShow.RoomName)).Id;
            var dto = (ShowsDTO)updatetedShow;
            SelectedShow = new ShowViewModel();
            return dto;
        }

        private RoomsDTO SetupRoomUpdate()
        {
            RoomViewModel updatetedRoom = new RoomViewModel();
            updatetedRoom.CopyFrom(SelectedRoom);

            var dto = (RoomsDTO)updatetedRoom;
            SelectedRoom = new RoomViewModel();
            RoomDetailsVisible?.Invoke(this, false);
            return dto;
        }

        private MoviesDTO SetupMovieUpdate()
        {
            MovieViewModel newMovie = new MovieViewModel();
            newMovie.CopyFrom(SelectedMovie);
            SelectedMovie = new MovieViewModel();
            var dto = (MoviesDTO)newMovie;
            MovieDetailsVisible?.Invoke(this, false);
            return dto;
        }

        private TicketsDTO SetupTicketUpdate()
        {
            TicketViewModel newTicket = new TicketViewModel();
            newTicket.CopyFrom(SelectedTicket);
            SelectedTicket = new TicketViewModel();
            var dto = (TicketsDTO)newTicket;
            TicketDetailsVisible?.Invoke(this, false);
            return dto;
        }

        private EmployeesDTO SetupUserUpdate()
        {
            EmployeeViewModel newUser = new EmployeeViewModel();
            newUser.CopyFrom(SelectedUser);
            SelectedUser = new EmployeeViewModel();
            var dto = (EmployeesDTO)newUser;
            UserDetailsVisible?.Invoke(this, false);
            return dto;
        }

        private StatsDTO SetupRoleUpdate()
        {
            StatsViewModel newRole = new StatsViewModel();
            newRole.CopyFrom(SelectedRole);
            SelectedRole = new StatsViewModel();
            var dto = (StatsDTO)newRole;
            RoleDetailsVisible?.Invoke(this, false);
            return dto;
        }

        private ProductDTO SetupProductUpdate()
        {
            ProductViewModel newRole = new ProductViewModel();
            newRole.CopyFrom(SelectedProduct);
            SelectedProduct = new ProductViewModel();
            var dto = (ProductDTO)newRole;
            ProductDetailsVisible?.Invoke(this, false);
            return dto;
        }


        #endregion

        #region Creates

        private async Task AddRole()
        {
            if (NewRole.Name != null)
            {
                var roleDTO = (StatsDTO)NewRole;
                if (roleDTO.Id == 0 && RolesList.Contains(roleDTO.Name))
                {
                    roleDTO.Id = _allRoles.FirstOrDefault(c => c.Name.Equals(roleDTO.Name)).Id;
                }
                roleDTO.UserId = SelectedUser.Id;
                NewRole.Id = await AddEntity("api/StatsAndPays", roleDTO);
                if (SelectedUser.Stats.FirstOrDefault(c => c.Id == NewRole.Id) == null)
                {
                    SelectedUser.Stats.Add(NewRole);
                }
                NewRole = new StatsViewModel();
            }
            await LoadRoles();
        }

        private async Task AddCategory()
        {
            if (_addMovie)
            {
                if (SelectedMovie.Categories == null)
                {
                    SelectedMovie.Categories = new ObservableCollection<CategoryViewModel>();
                }
                var category = _allCategories.FirstOrDefault(a => a.Category == NewCategory.Category);
                if (category != null)
                {
                    NewCategory.Id = category.Id;
                }
                SelectedMovie.Categories.Add(NewCategory);
                NewCategory = new CategoryViewModel();
            }
            else if (NewCategory.Category != null)
            {
                var categoryDTO = (CategoriesDTO)NewCategory;
                if (categoryDTO.Id == 0 && CategoriesList.Contains(categoryDTO.Category))
                {
                    categoryDTO.Id = _allCategories.FirstOrDefault(c => c.Category.Equals(categoryDTO.Category)).Id;
                }
                categoryDTO.MovieId = SelectedMovie.Id;
                NewCategory.Id = await AddEntity("api/Categories", categoryDTO);
                if (SelectedMovie.Categories.FirstOrDefault(c => c.Id == NewCategory.Id) == null)
                {
                    SelectedMovie.Categories.Add(NewCategory);
                }
                NewCategory = new CategoryViewModel();
            }
            await LoadCategories();
        }

        private async Task AddActor()
        {
            if (_addMovie)
            {
                if (SelectedMovie.Actors == null)
                {
                    SelectedMovie.Actors = new ObservableCollection<ActorViewModel>();
                }
                var actor = _allActors.FirstOrDefault(a => a.Name == NewActor.Name);
                if (actor != null)
                {
                    NewActor.Id = actor.Id;
                }
                SelectedMovie.Actors.Add(NewActor);
                NewActor.MainModel = this;
                NewActor = new ActorViewModel();
            }
            if (NewActor.Name != null && NewActor.Name != "")
            {
                var actorDTO = (ActorsDTO)NewActor;
                if (actorDTO.Id == 0 && ActorsList.Contains(actorDTO.Name))
                {
                    actorDTO.Id = _allActors.FirstOrDefault(a => a.Name == actorDTO.Name).Id;
                }
                actorDTO.MovieId = SelectedMovie.Id;
                NewActor.Id = await AddEntity("api/Actors", actorDTO);
                if (SelectedMovie.Actors.FirstOrDefault(a => a.Id == NewActor.Id) == null)
                {
                    SelectedMovie.Actors.Add(NewActor);
                }
                NewActor.MainModel = this;
                NewActor = new ActorViewModel();
            }
            await LoadActors();
        }

        public async Task AddNewProducts()
        {
            var dto = new ProductSellingDTO();
            dto.UserId = this.UserId;
            dto.Products = new List<ProductsForSell>();
            foreach (var product in ProductSell.ProductsCounter)
            {
                dto.Products.Add(new ProductsForSell { Count = product.Count, ProductId = product.Id});
            }
            await this.AddRent("api/BuffetWarehouses/sell", dto);
            await this.CreateProductsFieldAsync();
        }

        private async Task AddRent()
        {
            var dto = new RentFromGuestDTO();
            dto.UserId = TicketSell.SelectedUser != null ? TicketSell.SelectedUser.Id : 0;
            dto.IsEmployee = true;
            dto.ShowId = TicketSell.Show.Id;
            dto.Places = TicketSell.Places;
            dto.EmployeeId = UserId;
            await this.AddRent("api/Rents", dto);
            TicketSell = new TicketSellViewModel();
            await CreateMoviesFieldAsync();
        }

        private async Task AddCreatedShow()
        {
            if (SelectedShow.Date != DateTime.MinValue && SelectedShow.MovieTitle != "" && SelectedShow.RoomName != "")
            {
                var movie = Movies.FirstOrDefault(m => m.Title == SelectedShow.MovieTitle);
                String roomN = SelectedShow.RoomName.Split("(")[0].Trim();
                var room = Rooms.FirstOrDefault(r => r.Name.Equals(roomN));
                if (movie != null && room != null)
                {
                    SelectedShow.MovieId = movie.Id;
                    SelectedShow.RoomId = room.Id;
                    var showDTO = (ShowsDTO)SelectedShow;

                    SelectedShow.Id = await AddEntity("api/Shows", showDTO);
                    SelectedShow = new ShowViewModel();
                }
            }
        }

        private async Task AddCreatedMovie()
        {
            if (SelectedMovie.Title != null)
            {
                var movieDTO = (MoviesDTO)SelectedMovie;
                movieDTO.Actors.Select(a => new ActorsDTO { Name = a.Name, Id = a.Id });
                movieDTO.Categories.Select(a => new CategoriesDTO { Category = a.Category, Id = a.Id });
                SelectedMovie.Id = await AddEntity("api/Movies", movieDTO);
                _addMovie = false;
                NewActor = new ActorViewModel();
                SelectedMovie = new MovieViewModel();
                MovieDetailsVisible?.Invoke(this, false);
            }
        }

        private async Task AddCreatedRoom()
        {
            if (SelectedRoom.Name != null && SelectedRoom.Heigth != 0 && SelectedRoom.Width != 0)
            {
                var roomDTO = (RoomsDTO)SelectedRoom;
                SelectedRoom.Id = await AddEntity("api/Rooms", roomDTO);
                _addRoom = false;
                SelectedRoom = new RoomViewModel();
                await LoadRooms();
                RoomDetailsVisible?.Invoke(this, false);
            }
        }

        private async Task AddCreatedTicket()
        {
            if (SelectedTicket.Type != null && SelectedTicket.Price != 0)
            {
                var ticketDTO = (TicketsDTO)SelectedTicket;
                SelectedRoom.Id = await AddEntity("api/Tickets", ticketDTO);
                _addTicket = false;
                SelectedTicket = new TicketViewModel();
                await LoadTickets();
                TicketDetailsVisible?.Invoke(this, false);
            }
        }

        private async Task AddCreatedUser()
        {
            if (SelectedUser.Name != null && SelectedUser.UserName != null)
            {
                var userDTO = (EmployeesDTO)SelectedUser;
                SelectedUser.Id = await AddEntity("api/Users/", userDTO);
                _addUser = false;
                SelectedUser = new EmployeeViewModel();
                await LoadUsers();
                UserDetailsVisible?.Invoke(this, false);
            }
        }

        private async Task AddCreatedProduct()
        {
            if (SelectedProduct.Name != "" && SelectedProduct.Price != 0 && SelectedProduct.Quantity != 0)
            {
                var productDTO = (ProductDTO)SelectedProduct;
                SelectedProduct.Id = await AddEntity("api/BuffetWarehouses/", productDTO);
                _addProduct = false;
                SelectedProduct = new ProductViewModel();
                await LoadProducts();
                ProductDetailsVisible?.Invoke(this, false);
            }
        }
        #endregion

        #region Generics
        public async Task DeleteEntity(String route, int id, params Func<Task>[] loadMethods)
        {
            try
            {
                await _service.DeleteAsync(route, id);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            await Loads(loadMethods);
        }

        public async Task DeleteEntity(String route, int id, int id2, params Func<Task>[] loadMethods)
        {
            try
            {
                await _service.DeleteAsync(route, id,id2);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            await Loads(loadMethods);
        }

        private void ShowDetails(ViewModelBase selectedItem, ref bool adder, EventHandler<bool> e)
        {
            if (selectedItem != null)
            {
                selectedItem.MainModel = this;
                adder = false;
                e?.Invoke(this, true);
            }
        }

        private void Adder<T>(T selectedItem, ref bool adder, EventHandler<bool> e)
        {
            adder = !adder;
            e?.Invoke(this, adder);
        }

        private async Task Loads(Func<Task>[] loadMethods)
        {
            if (loadMethods != null)
            {
                foreach (var method in loadMethods)
                {
                    await method.Invoke();
                }
            }
        }


        private async Task UpdateSelectedItem<X>(bool adder, Func<Task> createMethod, bool condition, Func<X> setup, string route, params Func<Task>[] loadMethods) where X : RespondDTO
        {
            if (adder)
            {
                await createMethod();
            }
            else if (condition)
            {
                var dto = setup();
                await UpdateEntity(route, dto);
            }
            await Loads(loadMethods);

        }

        private async Task UpdateEntity<T>(String route, T entity) where T : RespondDTO
        {
            try
            {
                await _service.UpdateAsync(route, entity);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async Task AddRent<T>(String route, T entity) where T : RespondDTO
        {
            try
            {
                await _service.CreateRent(route, entity);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async Task<int> AddEntity<T>(String route, T entity) where T : RespondDTO
        {
            try
            {
                await _service.CreateAsync(route, entity);
                return entity.Id;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            return 0;
        }
        #endregion




    }
}
