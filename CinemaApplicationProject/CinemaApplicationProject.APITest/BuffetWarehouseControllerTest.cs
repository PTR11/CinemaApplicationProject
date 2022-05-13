using CinemaApplicationProject.API.Controllers;
using CinemaApplicationProject.Model;
using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using CinemaApplicationProject.Model.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace CinemaApplicationProject.APITest
{
    [Collection("Sequential")]
    public class BuffetWarehouseControllerTest : IDisposable
    {

        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly BuffetWarehousesController _controller;

        public BuffetWarehouseControllerTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DatabaseContext(options);
            TestDbInitializer.Initialize(_context);

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser, StatsAndPays, DatabaseContext, int>(_context), null,
                new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null);

           

            _service = new DatabaseService(_context, userManager);
            _controller = new BuffetWarehousesController(_service);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testName"),
                new Claim(ClaimTypes.NameIdentifier, "12"),
            });

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetWarehouseTest()
        {
            var result = _controller.GetBuffetWarehouse();


            var content = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(result.Value);

            Assert.Equal(3, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetWarehouseByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetProductById(id);

            // Assert
            var content = Assert.IsAssignableFrom<ProductDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidWarehouseTest()
        {
            // Arrange
            var id = 4;

            // Act
            var result = _controller.GetProductById(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }


        [Fact]
        public void GetWarehouseStatTest()
        {
            var result = _controller.GetStat();


            var content = Assert.IsAssignableFrom<IEnumerable<ProductStatDTO>>(result.Value);

            Assert.Equal(0, content.Count());
        }

        [Fact]
        public void PutWarehouseItemTest()
        {
            ProductDTO product = new ProductDTO()
            {
                Id = 1,
                Name = "perec",
                Price = 1,
                Quantity = 155
            };


            var result = _controller.PutProduct(1, product);
            Assert.IsAssignableFrom<OkResult>(result.Result);
            Assert.Equal("perec", _context.BuffetWarehouse.FirstOrDefault(b => b.Id == 1).Product.Name);
            Assert.Equal(1, _context.BuffetWarehouse.FirstOrDefault(b => b.Id == 1).Product.Price);
        }

        [Fact]
        public void PostWarehouseItemTest()
        {
            ProductDTO product = new ProductDTO()
            {
                Id = 0,
                Name = "kakaos csiga",
                Price = 122,
                Quantity = 56
            };

            var count = _context.BuffetWarehouse.Count();

            var result = _controller.PostProduct(product);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            Assert.IsAssignableFrom<ProductDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.BuffetWarehouse.Count());
            
        }

        [Fact]
        public void DeleteWarehouseItemTest()
        {
            var count = _context.BuffetWarehouse.Count();

            var result = _controller.DeleteProduct(1);

            Assert.IsAssignableFrom<NoContentResult>(result);

            Assert.Equal(count - 1, _context.BuffetWarehouse.Count());
        }



    }
}
