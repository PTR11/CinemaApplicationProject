using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class Shows : DatabaseBase
    {

        public int RoomId { get; set; }

        public int MovieId { get; set; }

        public Boolean IsActiveShow { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public Rooms Room { get; set; }

        public Movies Movie { get; set; }

    }
}
