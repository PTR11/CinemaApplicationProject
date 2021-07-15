using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Guests
    {
        public Guests()
        {
            Rent = new HashSet<Rents>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Rents> Rent { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
