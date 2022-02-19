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

namespace CinemaApplicationProject.Desktop.Viewmodel.Models
{
    public class MainViewModel : ViewModelBase
    {
        #region 
        #endregion
        private ObservableCollection<MovieViewModel> _movies = new ObservableCollection<MovieViewModel>();

        private ObservableCollection<ShowViewModel> _shows = new ObservableCollection<ShowViewModel>();

        private ObservableCollection<ActorViewModel> _actors = new ObservableCollection<ActorViewModel>();

        private ShowWeekViewModel _week = new ShowWeekViewModel();

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

        public event EventHandler AddColumnEvent;

        public DelegateCommand SelectMovie { get; set; }
        public DelegateCommand AddNewActor { get; set; }

        public DelegateCommand AddNewMovie { get; set; }

        public MainViewModel()
        {
            NewActor = new ActorViewModel();
            SelectedMovie = new MovieViewModel();
            SelectMovie = new DelegateCommand(_ => ShowMovieDetails(SelectedMovie));
            AddNewActor = new DelegateCommand(_ => AddActor());
            AddNewMovie = new DelegateCommand(_ => ShowMovieDetails(new MovieViewModel()));
            LoadInit();
        }

        private void ShowMovieDetails(MovieViewModel selectedMovie)
        {
            SelectedMovie = selectedMovie.ShallowClone();
            LoadActors();
            AddColumnEvent?.Invoke(this, EventArgs.Empty);
        }

        private async void LoadInit()
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
            Debug.WriteLine("faszomat");
            
        }

        private async void LoadActors()
        {
            if(SelectedMovie != null)
            {
                List<ActorViewModel> tmpActors = new List<ActorViewModel>();
                try
                {
                    tmpActors = new List<ActorViewModel>((await _service.LoadingAsync<ActorsDTO>("api/Actors/movie/" + SelectedMovie.Id))
                        .Select(entity => (ActorViewModel)entity));
                }
                catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
                {
                    OnMessageApplication($"Unexpected error occured! ({ex.Message})");
                }
                tmpActors.Add(new ActorViewModel { Name = "Add new actor..." });
                Actors = new ObservableCollection<ActorViewModel>(tmpActors);
            }
        }

        private async void AddActor()
        {

            var actorDTO = (ActorsDTO)NewActor;
            actorDTO.MovieId = SelectedMovie.Id;
            try
            {
                await _service.CreateAsync(actorDTO);
                NewActor.Id = actorDTO.Id;
                Actors.Add(NewActor);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }


    }
}
