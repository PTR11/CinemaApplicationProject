using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class CategoriesDTO : RespondDTO
    {
        public int Id { get; set; }

        public string Category { get; set; }



        public static explicit operator CategoriesDTO(Categories m) => new CategoriesDTO
        {
            Id = m.Id,
            Category = m.Category,
        };

    }
}
