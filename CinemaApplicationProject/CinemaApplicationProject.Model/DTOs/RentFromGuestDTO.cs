using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class Place : RespondDTO
    {
        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

        [Required]
        public String TicketCategory { get; set; }
    }

    public class RentFromGuestDTO : RespondDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ShowId { get; set; }

        public bool IsEmployee { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        public List<Place> Places { get; set; }


    }
}
