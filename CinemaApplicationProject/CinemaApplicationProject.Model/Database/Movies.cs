﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }

        public int Length { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        public String Trailer { get; set; }

        public Movies()
        {
            Actors = new HashSet<Actors>();
            Shows = new HashSet<Shows>();
            Categories = new HashSet<Categories>();
        }

        public ICollection<Actors> Actors { get; set; }

        public ICollection<Shows> Shows { get; set; }

        public ICollection<Categories> Categories { get; set; }
    }
}