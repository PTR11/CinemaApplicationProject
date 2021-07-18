using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class MoviesStatistics
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Week { get; set; }

        public int AverageRating { get; set; }

        public int ViewersNumber { get; set; }


        public Movies Movie { get; set; }
    }
}
