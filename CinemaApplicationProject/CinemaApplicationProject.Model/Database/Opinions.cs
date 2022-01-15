using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Opinions
    {
        [Key]
        public int Id { get; set; }

        public int Ranking { get; set; }

        public int MovieId { get; set; }

        public int GuestId { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        public Movies Movie { get; set; }
        public Guests Guest { get; set; }
    }
}
