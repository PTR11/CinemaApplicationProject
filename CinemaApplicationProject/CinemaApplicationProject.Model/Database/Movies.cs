﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Movies : DatabaseBase
    {

        public String Title { get; set; }

        public int Length { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        public String Director { get; set; }

        public String Trailer { get; set; }

        public byte[] Image { get; set; }

        public Movies()
        {
            Actors = new HashSet<Actors>();
            Shows = new HashSet<Shows>();
            Categories = new HashSet<Categories>();
        }

        public ICollection<Actors> Actors { get; set; }

        public ICollection<Shows> Shows { get; set; }

        public ICollection<Categories> Categories { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Movies movie = (Movies)obj;
                return (Title == movie.Title) && (Length == movie.Length);
            }
        }
    }

}
