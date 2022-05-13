using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoryControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly CategoriesController _controller;

        public CategoryControllerTest()
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
            _controller = new CategoriesController(_service);

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
        public void GetCategoriesTest()
        {
            var result = _controller.GetCategories();


            var content = Assert.IsAssignableFrom<IEnumerable<CategoriesDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(8, content.Count());
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetCategoryByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetCategoryById(id);

            // Assert
            var content = Assert.IsAssignableFrom<CategoriesDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidCategoryTest()
        {
            // Arrange
            var id = 29;

            // Act
            var result = _controller.GetCategoryById(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void ConnectCategoryToMovies()
        {
            var movieCount = _context.Movies.FirstOrDefault(m => m.Id == 1).Categories.Count;
            CategoriesDTO tmp = new CategoriesDTO()
            {
                Id = 0,
                MovieId = 1,
                Category = "Tmp cat"
            };

            var count = _context.Categories.Count();

            // Act
            var result = _controller.PostCategory(tmp);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<CategoriesDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Categories.Count());
            Assert.Equal(movieCount + 1, _context.Movies.FirstOrDefault(m => m.Id == 1).Categories.Count);
        }

        [Fact]
        public void DeleteCategory()
        {
            var cCount = _context.Categories.FirstOrDefault(a => a.Id == 2).Movies.Count;
            var mCount = _context.Movies.FirstOrDefault(m => m.Id == 1).Categories.Count;
            var result = _controller.DeleteCategory(1, 2);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(cCount - 1, _context.Categories.FirstOrDefault(a => a.Id == 2).Movies.Count);
            Assert.Equal(mCount - 1, _context.Movies.FirstOrDefault(m => m.Id == 1).Categories.Count);
        }
    }
}
