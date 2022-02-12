using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class GuestsDTO : MainDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; }

        [Required(ErrorMessage = "Credit Card Number is required")]
        public String CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8,ErrorMessage = "Password is too short")]
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

    public class LoginGuestDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password is too short")]
        public String Password { get; set; }
    }
}
