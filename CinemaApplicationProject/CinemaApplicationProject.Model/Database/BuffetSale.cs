using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class BuffetSale : DatabaseBase
    {

        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public virtual Products Product { get; set; }

        public virtual Employees Employee { get; set; }
    }
}
