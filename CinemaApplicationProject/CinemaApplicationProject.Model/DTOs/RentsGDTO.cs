using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class RentsGDTO : RespondDTO
    {

        public int X { get; set; }

        public int Y { get; set; }

        public static explicit operator RentsGDTO(Rents m) => new RentsGDTO
        {
            Id = m.Id,
            X = m.X,
            Y = m.Y
        };
    }
}
