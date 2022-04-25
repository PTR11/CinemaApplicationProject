using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApplicationProject.Model;
using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.Services;
using CinemaApplicationProject.Model.DTOs;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuffetWarehousesController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public BuffetWarehousesController(IDatabaseService service)
        {
            _service = service;
            DatabaseManipulation.context = _service.GetContext();
        }

        // GET: api/BuffetWarehouses
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetBuffetWarehouse()
        {
            return _service.GetWarehouse().Select(m => (ProductDTO)m).ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetBuffetWarehouseById(int id)
        {

            var item = _service.GetProductInWareHouse(id);

            if (item == null)
            {
                return NotFound();
            }

            return (ProductDTO)item;
           
        }

        [HttpGet("statistics")]
        public ActionResult<IEnumerable<ProductStatDTO>> GetStat()
        {
            return _service.ProductStatistics();
        }

        //// PUT: api/BuffetWarehouses/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<ProductDTO> PutProduct(int id,ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var tmp = _service.GetProductById(id);
            tmp.Product.Name = product.Name;
            tmp.Product.Price = product.Price;
            tmp.Product.Image = product.Image;
            tmp.Quantity = product.Quantity;
            if (DatabaseManipulation.UpdateElementAsync(tmp))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/BuffetWarehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ProductDTO> PostProduct(ProductDTO product)
        {
            var p = _service.GetProductByName(product.Name);
            if(p == null)
            {
                var tmpProduct = DatabaseManipulation.AddElement(new Products { Name = product.Name, Price = product.Price, Image = product.Image });
                var tmpB = DatabaseManipulation.AddElement(new BuffetWarehouse { Product = tmpProduct, Quantity = product.Quantity });
                if (tmpB == null || tmpProduct == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    return CreatedAtAction(nameof(GetBuffetWarehouseById), new { id = tmpB.Id }, (ProductDTO)tmpB);
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // POST: api/BuffetWarehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("sell")]
        public ActionResult<ProductSellingDTO> PostProductList(ProductSellingDTO product)
        {
            var p = _service.SellProducts(product);
            if (p)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }



        // DELETE: api/BuffetWarehouses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var delete = DatabaseManipulation.DeleteElement(product);
            if (!delete)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
