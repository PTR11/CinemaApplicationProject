using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public int Price { get; set; }
    }
}
