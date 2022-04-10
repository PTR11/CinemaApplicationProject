using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class ProductSeller
    {
        public int EmployeeId { get; set; }

        public String EmployeeName { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

    }

    public class ProductStatDTO : RespondDTO
    {
        public String ProductName { get; set; }

        public Double AverageSell { get; set; }

        public int AllSent { get; set; }
        

        public List<ProductSeller> ProductSellerList { get; set;}

    }
}
