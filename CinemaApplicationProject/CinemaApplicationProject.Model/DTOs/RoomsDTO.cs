using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class RoomsDTO : RespondDTO
    {

        public string Name { get; set; }

        public int Width { get; set; }

        public int Heigth { get; set; }

        public static explicit operator RoomsDTO(Rooms m) => new RoomsDTO
        {
            Id = m.Id,
            Name = m.Name,
            Width = m.Width,
            Heigth = m.Heigth
        };

        public static explicit operator Rooms(RoomsDTO m) => new Rooms
        {
            Id = m.Id,
            Name = m.Name,
            Width = m.Width,
            Heigth = m.Heigth
        };
    }
}
