using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        public Employees()
        {
            Presence = new HashSet<EmployeePresence>();
            Stat = new HashSet<StatsAndPays>();
            Rent = new HashSet<Rents>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        public ICollection<EmployeePresence> Presence { get; set; }

        public ICollection<StatsAndPays> Stat { get; set; }

        public ICollection<Rents> Rent { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
