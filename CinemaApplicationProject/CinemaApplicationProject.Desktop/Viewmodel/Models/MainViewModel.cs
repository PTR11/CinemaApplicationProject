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

        private ObservableCollection<String> _actorsList = new ObservableCollection<String>();

        private List<ActorViewModel> _allActors = new List<ActorViewModel>();

        private ShowWeekViewModel _week = new ShowWeekViewModel();

        private bool _addMovie = false;

        private MovieViewModel _selectedMovie;

        private ActorViewModel _newActor;

        public List<ShowViewModel> _list;

        public String name = "faszom";

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

        public ActorViewModel NewActor
        {
            get { return _newActor; }
            set { _newActor = value; OnPropertyChanged(); }
        }

        public event EventHandler<bool> MovieDetailsVisible;

        public DelegateCommand SelectMovie { get; set; }
        public DelegateCommand AddNewActor { get; set; }
        public DelegateCommand AddNewMovie { get; set; }
        public DelegateCommand UpdateMovie { get; set; }

        public MainViewModel()
        {
            NewActor = new ActorViewModel();
            SelectedMovie = new MovieViewModel();
            SelectMovie = new DelegateCommand(_ => !(SelectedMovie is null), _ => ShowMovieDetails(SelectedMovie));
            AddNewActor = new DelegateCommand(_ => !(NewActor is null), _ => AddActor());
            AddNewMovie = new DelegateCommand(_ => AddMovie());
            UpdateMovie = new DelegateCommand(_ => !(SelectedMovie is null), _ => UpdateSelectedMovie(SelectedMovie));
            LoadInit();
        }

        private void ShowMovieDetails(MovieViewModel selectedMovie)
        {
            if(selectedMovie != null)
            {
                SelectedMovie = selectedMovie.ShallowClone();
                MovieDetailsVisible?.Invoke(this, true);
            }
        }

        private async void LoadInit()
        {
            await LoadMovies();
            await LoadShows();
            await LoadActors();

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

            DateTime startOfWeek = DateTime.Today.AddDays(
                (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
                (int)DateTime.Today.DayOfWeek);

            foreach (var show in tmpShows)
            {
                switch (show.Date.DayOfWeek.ToString())
                {
                    case "Monday":
                        Week.Monday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek));
                        break;
                    case "Tuesday":
                        Week.Tuesday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek));
                        break;
                    case "Wednesday":
                        Week.Wednesday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek));
                        break;
                    case "Thursday":
                        Week.Thursday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek));
                        break;
                    case "Friday":
                        Week.Friday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek));
                        break;
                    case "Saturday":
                        Week.Saturday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek));
                        break;
                    case "Sunday":
                        Week.Sunday = new ObservableCollection<ShowViewModel>(tmpShows.Where(m => m.Date.DayOfWeek == show.Date.DayOfWeek));
                        break;
                }
            }
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

        private void AddMovie()
        {
            SelectedMovie = new MovieViewModel();
            SelectedMovie.Shows = new List<ShowViewModel>();
            SelectedMovie.Actors = new ObservableCollection<ActorViewModel>();
            
            _addMovie = true;
            MovieDetailsVisible?.Invoke(this, true);
        }

        private async void AddActor()
        {
            if (_addMovie && SelectedMovie.Actors == null)
            {
                SelectedMovie.Actors = new ObservableCollection<ActorViewModel>();
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
                SelectedMovie.Actors.Add(NewActor);
                NewActor = new ActorViewModel();
            }
            
        }

        private async void UpdateSelectedMovie(MovieViewModel selectedMovie)
        {
            if (_addMovie)
            {
                await AddCreatedMovie();
            }
            if (selectedMovie.Title != null)
            {
                MovieViewModel newMovie = new MovieViewModel();
                newMovie.CopyFrom(selectedMovie);
                
                await UpdateEntity<MoviesDTO>((MoviesDTO)newMovie);
                
                selectedMovie = new MovieViewModel();
                await LoadMovies();
                MovieDetailsVisible?.Invoke(this, false);
            }
        }

        private async Task AddCreatedMovie()
        {
            if (SelectedMovie.Title != null)
            {
                var movieDTO = (MoviesDTO)SelectedMovie;
                movieDTO.Actors.Select(a => new ActorsDTO { Name = a.Name, Id = a.Id });
                SelectedMovie.Id = await AddEntity("api/Movies",movieDTO);
                _addMovie = false;
                SelectedMovie.Actors.Add(NewActor);
                NewActor = new ActorViewModel();
            }
        }

        private async Task UpdateEntity<T>(T entity) where T : RespondDTO
        {
            try
            {
                await _service.UpdateAsync("api/Movies",entity);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            //LoadActorsAsync(SelectedMovie);
        }

        private async Task<int> AddEntity<T>(String route,T entity) where T : RespondDTO
        {
            try
            {
                await _service.CreateAsync(route,entity);
                return entity.Id;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            return 0;
        }


    }
}
