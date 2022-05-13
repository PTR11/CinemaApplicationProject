using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Guests : ApplicationUser
    {
        public String CreditCardNumber { get; set; }
        public Guests()
        {
            Rent = new HashSet<Rents>();
            Opinions = new HashSet<Opinions>();
        }
        
        public virtual ICollection<Rents> Rent { get; set; }
        
        public virtual ICollection<Opinions> Opinions { get; set; }
    }
}
