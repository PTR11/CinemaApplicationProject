using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class BuffetSale
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public Products Product { get; set; }

        public Employees Employee { get; set; }
    }
}
