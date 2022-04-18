using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class GuestVDTO : RespondDTO
    {
        public int Id { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }

        public static explicit operator GuestVDTO(Guests m) => new GuestVDTO
        {
            Id = m.Id,
            UserName = m.UserName,
            Email = m.Email,
        };
    }
}
