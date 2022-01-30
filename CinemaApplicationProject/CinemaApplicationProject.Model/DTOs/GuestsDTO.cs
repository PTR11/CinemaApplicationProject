using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class GuestsDTO
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String CreditCardNumber { get; set; }
        public String Password { get; set; }

        public static explicit operator GuestsDTO(Guests m) => new GuestsDTO
        {
            Id = m.Id,
            UserName = m.UserName,
            Name = m.Name,
            Email = m.Email,
            Address = m.Address,
            CreditCardNumber = m.CreditCardNumber,
        };
    }
}
