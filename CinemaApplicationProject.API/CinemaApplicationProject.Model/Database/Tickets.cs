using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Tickets
    {
        [Key]
        public int Id { get; set; }

        public String Type { get; set; }

        public int Price { get; set; }
    }
}
