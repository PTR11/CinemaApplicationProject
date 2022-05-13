using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Rooms : DatabaseBase
    {

        public String Name { get; set; }

        public int Width { get; set; }

        public int Heigth { get; set; }

        public Rooms()
        {
            Shows = new HashSet<Shows>();
        }

        public virtual ICollection<Shows> Shows { get; set; }



    }
}
