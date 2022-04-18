using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class RentsDTO : RespondDTO
    {
        public int Id { get; set; }
        public int X { get; set; }

        public int Y { get; set; }

        public int TicketId { get; set; }

        public int? EmployeeId { get; set; }

        public int? GuestId { get; set; }

        public GuestVDTO Guest { get; set; }

        public static explicit operator RentsDTO(Rents m) => new RentsDTO
        {
            Id = m.Id,
            X = m.X,
            Y = m.Y,
            TicketId = m.TicketId,
            EmployeeId = m.EmployeeId,
            GuestId = m.GuestId,
            Guest = m.Guest == null? null : (GuestVDTO) m.Guest
        };
    }
}
