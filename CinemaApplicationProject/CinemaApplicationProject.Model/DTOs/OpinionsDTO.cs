using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class OpinionsDTO
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public Boolean Anonymus { get; set; }

        public String GuestName { get; set; }
        [Required]
        public int GuestId { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public int Ranking { get; set; }

        public static explicit operator OpinionsDTO(Opinions m) => new OpinionsDTO
        {
            MovieId = m.MovieId,
            GuestId = m.GuestId,
            Description = m.Description,
            Ranking = m.Ranking,
            Anonymus = m.Anonymus,
            GuestName = m.Anonymus ? m.Guest.UserName : "Anonymus",
        };


    }
}
