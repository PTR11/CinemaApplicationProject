using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class ShowsDTO : RespondDTO
    {

        public int RoomId { get; set; }

        public int MovieId { get; set; }

        public MoviesDTO Movie { get; set; } 

        public RoomsDTO Room { get; set; }
        
        public DateTime Date { get; set; }

        public static explicit operator ShowsDTO(Shows m) => new ShowsDTO
        {
            Id = m.Id,
            RoomId = m.RoomId,
            MovieId = m.MovieId,
            Date = m.Date,
            Movie = m.Movie!= null ? new MoviesDTO
            {
                Id = m.Movie.Id,
                Title = m.Movie.Title,
                Length = m.Movie.Length
            } : null,
            Room = m.Room != null ? new RoomsDTO
            {
                Id = m.Room.Id,
                Name = m.Room.Name,
                Width = m.Room.Width,
                Heigth = m.Room.Heigth,
            } : null,
        };

        public static explicit operator Shows(ShowsDTO m) => new Shows
        {
            Id = m.Id,
            RoomId = m.RoomId,
            MovieId = m.MovieId,
            Date = m.Date,
        };




    }
}
