using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class PlacesDTO
    {
        public class Place : RespondDTO
        {
            public int X { get; set; }

            public int Y { get; set; }

            public String TicketCategory { get; set; }
        }
    }
}
