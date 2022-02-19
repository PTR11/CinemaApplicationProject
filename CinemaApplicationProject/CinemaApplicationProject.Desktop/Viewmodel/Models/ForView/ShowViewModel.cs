using CinemaApplicationProject.Desktop.Model.Services;
using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ShowViewModel : ViewModelBase
    {
        public int _id;
        public int _roomId;
        public string _roomName;
        public int _movieId;
        public string _movieTitle;
        public DateTime _date;

        public Int32 Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public String MovieTitle
        {
            get { return _movieTitle; }
            set { _movieTitle = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }
        public Int32 RoomId
        {
            get { return _roomId; }
            set { _roomId = value; OnPropertyChanged(); }
        }

        public Int32 MovieId
        {
            get { return _movieId; }
            set { _movieId = value; OnPropertyChanged(); }
        }

        public String RoomName
        {
            get { return _roomName; }
            set { _roomName = value; OnPropertyChanged(); }
        }

        public ShowViewModel ShallowClone()
        {
            return (ShowViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(ShowViewModel rhs)
        {
            Id = rhs.Id;
            MovieTitle = rhs.MovieTitle;
            RoomName = rhs.RoomName;
            MovieId = rhs.MovieId;
            RoomId = rhs.RoomId;
            Date = rhs.Date;
        }

        public static explicit operator ShowViewModel(ShowsDTO dto) => new ShowViewModel
        {
            Id = dto.Id,
            MovieId = dto.MovieId,
            RoomId = dto.RoomId,
            Date = dto.Date,
            MovieTitle = dto.Movie != null ? dto.Movie.Title : "",
            RoomName = dto.Room != null ? dto.Room.Name : "",
        };

        public static explicit operator ShowsDTO(ShowViewModel mvm) => new ShowsDTO
        {
            Id = mvm.Id,
            RoomId = mvm.RoomId,
            MovieId = mvm.MovieId,
            Date = mvm.Date
        };

        public static async Task<String> GetMovieTitleAsync(int id)
        {
            var movie = await _service.GetAsync<MoviesDTO>("api/Movies/" + id);

            return movie.Title;
        }

        public static String GetRoomName(int id) => _service.GetAsync<RoomsDTO>("api/Rooms/" + id).Result.Name;


    }
}
