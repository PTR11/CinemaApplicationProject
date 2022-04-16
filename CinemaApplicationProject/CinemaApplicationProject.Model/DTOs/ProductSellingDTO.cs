using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class ProductsForSell : RespondDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Count { get; set; }
    }
    public class ProductSellingDTO : RespondDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<ProductsForSell> Products { get; set; }
    }
}
