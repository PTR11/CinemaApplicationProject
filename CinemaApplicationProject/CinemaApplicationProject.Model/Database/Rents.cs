using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Rents : DatabaseBase
    {

        public int ShowId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int TicketId { get; set; }

        public int?  EmployeeId { get; set; }

        public int GuestId { get; set; }

        public virtual Employees Employee { get; set; }

        public Guests Guest { get; set; }

        public Tickets Ticket { get; set; }


    }
}
