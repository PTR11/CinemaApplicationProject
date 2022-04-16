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
        //public int StatId { get; set; }
        public Double DutyTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Login { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Logout { get; set; }

        public virtual Employees Employee { get; set; }
        //public StatsAndPays Stat { get; set; }



    }
}
