using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class MovieViewModel : ViewModelBase
    {
        private int _id;
        private String _title;
        private String _director;
        private int _length;
        private String _description;
        public String _trailer;
        public List<ShowViewModel> _shows;
        public ObservableCollection<ActorViewModel> _actors;
        public ObservableCollection<CategoryViewModel> _categories;

        public MovieViewModel()
        {
            this.Id = 0;
            this.Title = "";
            this.Director = "";
            this.Length = 0;
            this.Description = "";
            this.Trailer = "";
            this.Shows = new List<ShowViewModel>();
            this.Actors = new ObservableCollection<ActorViewModel>();
            this.Categories = new ObservableCollection<CategoryViewModel>();
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public String Director
        {
            get { return _director; }
            set { _director = value; OnPropertyChanged(); }
        }

        public Int32 Length
        {
            get { return _length; }
            set { _length = value; OnPropertyChanged(); }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        public String Trailer
        {
            get { return _trailer; }
            set { _trailer = value; OnPropertyChanged(); }
        }

        public List<ShowViewModel> Shows
        {
            get { return _shows; }
            set { _shows = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ActorViewModel> Actors
        {
            get { return _actors; }
            set { _actors = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }

        public MovieViewModel ShallowClone()
        {
            return (MovieViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(MovieViewModel rhs)
        {
            Id = rhs.Id;
            Title = rhs.Title;
            Director = rhs.Director;
            Length = rhs.Length;
            Description = rhs.Description;
            Trailer = rhs.Trailer;
            Shows = rhs.Shows;
            Actors = rhs.Actors;
            Categories = rhs.Categories;
        }

        public static explicit operator MovieViewModel(MoviesDTO dto) => new MovieViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Length = dto.Length,
            Description = dto.Description,
            Trailer = dto.Trailer,
            Shows = new(dto.Shows.ToList().Select(x => (ShowViewModel)x)),
            Actors = new(dto.Actors.ToList().Select(x => (ActorViewModel)x)),
            Categories = new(dto.Categories.ToList().Select(x => (CategoryViewModel)x)),
        };

        public static explicit operator MoviesDTO(MovieViewModel mvm) => new MoviesDTO
        {
            Id = mvm.Id,
            Title = mvm.Title,
            Length = mvm.Length,
            Description = mvm.Description,
            Trailer = mvm.Trailer,
            Shows = new(mvm.Shows.ToList().Select(x => (ShowsDTO)x)),
            Actors = new(mvm.Actors.ToList().Select(x => (ActorsDTO)x)),
            Categories = new(mvm.Categories.ToList().Select(x => (CategoriesDTO)x)),
        };
    }
}
