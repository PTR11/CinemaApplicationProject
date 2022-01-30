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
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int MovieId { get; set; }

        public MoviesDTO Movies { get; set; } 
        
        public DateTime Date { get; set; }

        public static explicit operator ShowsDTO(Shows m) => new ShowsDTO
        {
            Id = m.Id,
            RoomId = m.RoomId,
            MovieId = m.MovieId,
            Date = m.Date
        };
    }
}
