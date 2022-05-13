using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Categories : DatabaseBase
    {
        public string Category { get; set; }


        public virtual ICollection<Movies> Movies { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Categories p = (Categories)obj;
                return this.Category.Equals(p.Category);
            }
        }
    }
}
