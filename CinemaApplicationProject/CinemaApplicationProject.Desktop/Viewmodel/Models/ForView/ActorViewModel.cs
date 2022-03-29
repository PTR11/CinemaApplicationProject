using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ActorViewModel : ViewModelBase
    {
        public string _name;
        public int _movieId;

        public ActorViewModel()
        {
            this.Name = "";
            this.MovieId = 0;
            DeleteMovieActor = new DelegateCommand(async _ => await DeleteActor());
        }

        public async Task DeleteActor()
        {
            var main = (MainViewModel)this.MainModel;
            if (main._addMovie)
            {
                main.SelectedMovie.Actors.Remove(this);
            }
            else
            {
                await main.DeleteEntity("api/Actors", main.SelectedMovie.Id, this.Id, main.LoadInit);
                main.MovieVisibility(false);
            }
            
        }

        public DelegateCommand DeleteMovieActor { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public int MovieId
        {
            get { return _movieId; }
            set { _movieId = value; OnPropertyChanged(); }
        }

        public ActorViewModel ShallowClone()
        {
            return (ActorViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(ActorViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
            MovieId = rhs.MovieId;
        }

        public static explicit operator ActorViewModel(ActorsDTO dto) => new ActorViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            MovieId = dto.MovieId,
        };

        public static explicit operator ActorsDTO(ActorViewModel mvm) => new ActorsDTO
        {
            Id = mvm.Id,
            Name = mvm.Name,
            MovieId = mvm.MovieId
        };
    }
}
