using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class ProductDTO : RespondDTO
    {
        public String Name { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public static explicit operator ProductDTO(BuffetWarehouse m) => new ProductDTO
        {
            Id = m.Id,
            Name = m.Product.Name,
            Price = m.Product.Price,
            Quantity = m.Quantity,
            Image = m.Product.Image
        };

    }
}
