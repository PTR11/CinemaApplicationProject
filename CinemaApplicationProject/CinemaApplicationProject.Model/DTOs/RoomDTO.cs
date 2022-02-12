using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class RoomDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Width { get; set; }

        public int Heigth { get; set; }

        public static explicit operator RoomDTO(Rooms m) => new RoomDTO
        {
            Id = m.Id,
            Name = m.Name,
            Width = m.Width,
            Heigth = m.Heigth
        };
    }
}
