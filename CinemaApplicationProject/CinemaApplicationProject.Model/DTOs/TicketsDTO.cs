using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class TicketsDTO : RespondDTO
    {
        public String Type { get; set; }

        public int Price { get; set; }

        public static explicit operator TicketsDTO(Tickets m) => new TicketsDTO
        {
            Id = m.Id,
            Type = m.Type,
            Price = m.Price,
        };

        public static explicit operator Tickets(TicketsDTO m) => new Tickets
        {
            Id = m.Id,
            Type = m.Type,
            Price = m.Price,
        };
    }
}
