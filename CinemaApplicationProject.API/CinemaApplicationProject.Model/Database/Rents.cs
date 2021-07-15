﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Rents
    {
        [Key]
        public int Id { get; set; }

        public int ShowId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public String Type { get; set; }

        public int  EmployeeId { get; set; }

        public String GuestId { get; set; }

        public Employees Employee { get; set; }

        public Guests Guest { get; set; }


    }
}