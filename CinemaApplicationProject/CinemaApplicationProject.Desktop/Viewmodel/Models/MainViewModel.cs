using CinemaApplicationProject.Desktop.Model.Errors;
using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Desktop.Viewmodel.Models.ForView;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models
{
    public class MainViewModel : ViewModelBase
    {
        #region 
        #endregion
        private ObservableCollection<MovieViewModel> _movies = new ObservableCollection<MovieViewModel>();

        private ObservableCollection<ShowViewModel> _shows = new ObservableCollection<ShowViewModel>();

        private ObservableCollection<ActorViewModel> _actors = new ObservableCollection<ActorViewModel>();

        private ObservableCollection<RoomViewModel> _rooms = new ObservableCollection<RoomViewModel>();

        private ObservableCollection<TicketViewModel> _tickets = new ObservableCollection<TicketViewModel>();

        private ObservableCollection<String> _categoriesList = new ObservableCollection<String>();
        private ObservableCollection<String> _actorsList = new ObservableCollection<String>();
        private ObservableCollection<String> _moviesList = new ObservableCollection<String>();
        private ObservableCollection<String> _roomsList = new ObservableCollection<String>();

        private List<ActorViewModel> _allActors = new List<ActorViewModel>();

        private List<CategoryViewModel> _allCategories = new List<CategoryViewModel>();

        private ShowWeekViewModel _week = new ShowWeekViewModel();

        private bool _addMovie = false;

        private bool _addRoom = false;

        private bool _addTicket = false;

        private bool _addShow = false;

        private MovieViewModel _selectedMovie;

        private ShowViewModel _selectedShow;

        private RoomViewModel _selectedRoom;

        private TicketViewModel _selectedTicket;

        private ActorViewModel _newActor;

        private CategoryViewModel _newCategory;

        public List<ShowViewModel> _list;

        public String name = "faszom";

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

        public ObservableCollection<String> ActorsList
        {
            get { return _actorsList; }
            set { _actorsList = value; OnPropertyChanged(); }
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

        public event EventHandler<bool> MovieDetailsVisible;
        public event EventHandler<bool> RoomDetailsVisible;
        public event EventHandler<bool> TicketDetailsVisible;
        public DelegateCommand SelectMovie { get; set; }
        public DelegateCommand SelectRoom { get; set; }
        public DelegateCommand SelectTicket { get; set; }
        public DelegateCommand AddNewActor { get; set; }
        public DelegateCommand AddNewCategory { get; set; }
        public DelegateCommand AddNewMovie { get; set; }
        public DelegateCommand AddNewRoom { get; set; }
        public DelegateCommand AddNewTicket { get; set; }
        public DelegateCommand UpdateShow { get; set; }
        public DelegateCommand UpdateMovie { get; set; }
        public DelegateCommand UpdateRoom { get; set; }
        public DelegateCommand UpdateTicket { get; set; }
        public DelegateCommand LastWeek { get; set; }
        public DelegateCommand ActualWeek { get; set; }
        public DelegateCommand NextWeek { get; set; }

        public MainViewModel()
        {
            
            
            SelectMovie = new DelegateCommand( _ => ShowDetails(SelectedMovie,ref _addMovie, MovieDetailsVisible));
            SelectRoom = new DelegateCommand(_ => ShowDetails(SelectedRoom,ref _addRoom, RoomDetailsVisible));
            SelectTicket = new DelegateCommand(_ => ShowDetails(SelectedTicket, ref _addTicket, TicketDetailsVisible));


            AddNewActor = new DelegateCommand(_ => !(NewActor is null), async _ => await AddActor());
            AddNewCategory = new DelegateCommand(_ => !(NewActor is null),async _ => await AddCategory());

            AddNewMovie = new DelegateCommand(_ => Adder(SelectedMovie=new MovieViewModel(),ref _addMovie,MovieDetailsVisible));
            AddNewRoom = new DelegateCommand(_ => Adder(SelectedRoom=new RoomViewModel(),ref _addRoom,RoomDetailsVisible));
            AddNewTicket = new DelegateCommand(_ => Adder(SelectedTicket=new TicketViewModel(),ref _addTicket,TicketDetailsVisible));

            //Updates
            UpdateShow = new DelegateCommand(_ => !(SelectedShow is null), async _ => await UpdateSelectedItem(_addShow,AddCreatedShow, SelectedShow.Id != 0,SetupShowUpdate,"api/Shows",LoadShows,LoadRooms));

            UpdateMovie = new DelegateCommand(_ => !(SelectedMovie is null), async _ => await UpdateSelectedItem(_addMovie, AddCreatedMovie, SelectedMovie.Title != null, SetupMovieUpdate, "api/Movies", LoadMovies, LoadShows));

            UpdateRoom = new DelegateCommand(_ => !(SelectedRoom is null), async _ => await UpdateSelectedItem(_addRoom, AddCreatedRoom, SelectedRoom.Id != 0, SetupRoomUpdate, "api/Rooms", LoadRooms, LoadShows));


            UpdateTicket = new DelegateCommand(_ => !(SelectedTicket is null), async _ => await UpdateSelectedItem(_addTicket, AddCreatedTicket, SelectedTicket.Id != 0, SetupTicketUpdate, "api/Tickets", null));

            LastWeek = new DelegateCommand(_ => ChangeWeekToLast());
            ActualWeek = new DelegateCommand(async _ => await LoadShows());
            NextWeek = new DelegateCommand(_ => ChangeWeekToNext());

            NewActor = new ActorViewModel();
            NewCategory = new CategoryViewModel();

            SelectedShow = new ShowViewModel();
            SelectedMovie = new MovieViewModel();
            SelectedRoom = new RoomViewModel();
            SelectedTicket = new TicketViewModel();

           LoadInit();
        }

        private void ChangeWeekToNext()
        {
            DateTime mondayOfLastWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek+7);
            DateTime lastDayOfWeek = mondayOfLastWeek.AddDays(6);
            List<ShowViewModel> tmpShow = new List<ShowViewModel>(Shows.Where(m => mondayOfLastWeek.Ticks <= m.Date.Ticks && m.Date.Ticks <= lastDayOfWeek.Ticks).ToList());
            SeparateShowsByDay(tmpShow);
        }

        private void ChangeWeekToLast()
        {
            DateTime mondayOfLastWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6);
            DateTime lastDayOfWeek = mondayOfLastWeek.AddDays(6);
            List<ShowViewModel> tmpShow = new List<ShowViewModel>(Shows.Where(m => mondayOfLastWeek.Ticks <= m.Date.Ticks && m.Date.Ticks <= lastDayOfWeek.Ticks).ToList());
            SeparateShowsByDay(tmpShow);

        }

        

        private async Task LoadInit()
        {
            await LoadMovies();
            await LoadShows();
            await LoadActors();
            await LoadRooms();
            await LoadTickets();
            await LoadCategories();
        }

        

        private bool IsOnThisWeek(DateTime date)
        {
            DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek+1).Date;
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

        #region Loaders

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
            Movies = new ObservableCollection<MovieViewModel>(tmpMovies);
            MoviesList = new ObservableCollection<string>(tmpMovies.Select(m => m.Title).ToList());
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
            Rooms = new ObservableCollection<RoomViewModel>(tmpRooms);
            RoomsList = new ObservableCollection<string>(tmpRooms.Select(r => "" + r.Name + " (" + r.Width + "x" + r.Heigth + ")").ToList());
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
            CategoriesList = new ObservableCollection<string>(_allCategories.Select(a => a.Category).ToList());
        }

        #endregion


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
                    NewActor.Id = category.Id;
                }
                SelectedMovie.Categories.Add(NewCategory);
                NewActor = new ActorViewModel();
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
        }

        private async Task AddActor()
        {
            if (_addMovie)
            {
                if(SelectedMovie.Actors == null)
                {
                    SelectedMovie.Actors = new ObservableCollection<ActorViewModel>();
                }
                var actor = _allActors.FirstOrDefault(a => a.Name == NewActor.Name);
                if(actor != null)
                {
                    NewActor.Id = actor.Id;
                }
                SelectedMovie.Actors.Add(NewActor);
                NewActor = new ActorViewModel();
            }
            if (NewActor.Name != null)
            {
                var actorDTO = (ActorsDTO)NewActor;
                if (actorDTO.Id == 0 && ActorsList.Contains(actorDTO.Name))
                {
                    actorDTO.Id = _allActors.FirstOrDefault(a => a.Name == actorDTO.Name).Id;
                }
                actorDTO.MovieId = SelectedMovie.Id;
                NewActor.Id = await AddEntity("api/Actors",actorDTO);
                if(SelectedMovie.Actors.FirstOrDefault(a => a.Id == NewActor.Id) == null)
                {
                    SelectedMovie.Actors.Add(NewActor);
                }
                NewActor = new ActorViewModel();
            }
        }


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
            newMovie.Actors.Clear();
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
        #endregion

        #region Creates
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
                    await LoadShows();
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
                SelectedMovie.Id = await AddEntity("api/Movies", movieDTO);
                _addMovie = false;
                NewActor = new ActorViewModel();
                SelectedMovie = new MovieViewModel();
                await LoadMovies();
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
        #endregion

        #region Generics
        private void ShowDetails(ViewModelBase selectedItem, ref bool adder, EventHandler<bool> e)
        {
            if (selectedItem != null)
            {
                adder = false;
                e?.Invoke(this, true);
            }
        }

        private void Adder<T>(T selectedItem, ref bool adder, EventHandler<bool> e)
        {
            adder = !adder;
            e?.Invoke(this, adder);
        }

        private async Task UpdateSelectedItem<X>(bool adder, Func<Task> createMethod, bool condition, Func<X> setup, string route, params Func<Task>[] loadMethods) where X : RespondDTO
        {
            if (adder)
            {
                await createMethod();
                return;
            }
            else if (condition)
            {
                var dto = setup();
                await UpdateEntity(route, dto);
                if (loadMethods != null)
                {
                    foreach (var method in loadMethods)
                    {
                        await method.Invoke();
                    }
                }
            }
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
