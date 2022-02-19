using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class BuffetWarehouse : DatabaseBase
    {

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Products Product { get; set; }

    }
}
