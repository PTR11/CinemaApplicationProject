using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class ActorsDTO : RespondDTO
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public static explicit operator ActorsDTO(Actors m) => new ActorsDTO
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
