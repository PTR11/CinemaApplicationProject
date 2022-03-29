using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Actors : DatabaseBase
    {
        public String Name { get; set; }

        public Actors()
        {
            Movies = new HashSet<Movies>();
        }
        public virtual ICollection<Movies> Movies { get; set; }


        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Actors actor = (Actors)obj;
                return (Name.Equals(actor.Name));
            }
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
