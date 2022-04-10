using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class EmployeesStats : IdentityUserRole<int>
    {
        public virtual Employees User { get; set; }
        public virtual StatsAndPays Role { get; set; }
    }
}
