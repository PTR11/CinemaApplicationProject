using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class PresenceDTO : RespondDTO
    {

        public double DutyTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Login { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Logout { get; set; }


    }
}
