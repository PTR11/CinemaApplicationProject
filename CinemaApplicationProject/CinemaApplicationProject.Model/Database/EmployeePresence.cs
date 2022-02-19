using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class EmployeePresence : DatabaseBase
    {

        public int EmployeeId { get; set; }
        public int StatId { get; set; }
        public int DutyTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Day { get; set; }

        public Employees Employee { get; set; }
        public StatsAndPays Stat { get; set; }



    }
}
