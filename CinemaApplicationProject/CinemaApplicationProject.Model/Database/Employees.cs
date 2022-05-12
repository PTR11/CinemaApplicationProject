﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Employees : ApplicationUser
    {
        public Employees()
        {
            Presence = new HashSet<EmployeePresence>();
            Stat = new HashSet<StatsAndPays>();
            Rent = new HashSet<Rents>();
            BuffetSale = new HashSet<BuffetSale>();
        }

        public String Birthday { get; set; }

        public virtual ICollection<EmployeePresence> Presence { get; set; }

        public virtual ICollection<StatsAndPays> Stat { get; set; }

        public virtual ICollection<EmployeesStats> UserRole { get; set; }

        public virtual ICollection<Rents> Rent { get; set; }

        public virtual ICollection<BuffetSale> BuffetSale { get; set; }
    }
}
