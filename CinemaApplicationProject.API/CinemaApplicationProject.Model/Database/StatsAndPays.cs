using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class StatsAndPays : IdentityRole<int>
    {

        public StatsAndPays(String name) : base(name)
        {
            Salary = 0;
        }

        public int Salary { get; set; }
    }
}
