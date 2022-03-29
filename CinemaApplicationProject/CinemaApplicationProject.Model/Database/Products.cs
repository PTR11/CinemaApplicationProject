using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Products : DatabaseBase
    {

        public String Name { get; set; }

        public int Price { get; set; }

        public byte[] Image { get; set; }
    }
}
