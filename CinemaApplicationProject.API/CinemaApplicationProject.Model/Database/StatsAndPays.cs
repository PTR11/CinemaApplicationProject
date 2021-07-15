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
        [Key]
        public int Id { get; set; }
        public String StatId { get; set; }

        public int Salary { get; set; }
    }
}
