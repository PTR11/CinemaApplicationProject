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
    public class MoviesControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly MoviesController _controller;

        public MoviesControllerTest()
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
            _controller = new MoviesController(_service);

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
        public void GetMoviesTest()
        {
            var result = _controller.GetMovies();


            var content = Assert.IsAssignableFrom<IEnumerable<MoviesDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(7, content.Count());
        }

        [Fact]
        public void GetMoviesTodayTest()
        {
            var result = _controller.GetTodaysMovies();


            var content = Assert.IsAssignableFrom<IEnumerable<MoviesDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(1, content.Count());
        }

        [Fact]
        public void GetMoviesStatTest()
        {
            var result = _controller.GetStatistics();


            var content = Assert.IsAssignableFrom<IEnumerable<MoviesDTO>>(result.Value);

            Assert.Equal(7, content.Count());
        }

        [Fact]
        public void GetMoviesByTitlePartTest()
        {
            var result = _controller.GetMoviesByTitlePart("Star");


            var content = Assert.IsAssignableFrom<IEnumerable<MoviesDTO>>(result.Value);

            Assert.Equal(1, content.Count());
        }

        [Fact]
        public void GetMoviesByCategoryTest()
        {
            var result = _controller.GetMoviesByCategory("Action");


            var content = Assert.IsAssignableFrom<IEnumerable<MoviesDTO>>(result.Value);

            Assert.Equal(5, content.Count());
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetMovieByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetMovie(id);

            // Assert
            var content = Assert.IsAssignableFrom<MoviesDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidMovieTest()
        {
            // Arrange
            var id = 19;

            // Act
            var result = _controller.GetMovie(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void DeleteMovie()
        {
            var mCount = _context.Movies.Count();
            var result = _controller.DeleteMovie(1);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(mCount - 1, _context.Movies.Count());
        }

        [Fact]
        public void PutMoviesItemTest()
        {
            MoviesDTO movie = new MoviesDTO()
            {
                Id = 3,
                Title = "perec",
                Length = 1
            };


            var result = _controller.PutMovie(3, movie);
            Assert.IsAssignableFrom<OkResult>(result.Result);
            Assert.Equal("perec", _context.Movies.FirstOrDefault(b => b.Id == 3).Title);
            Assert.Equal(1, _context.Movies.FirstOrDefault(b => b.Id == 3).Length);
        }

        [Fact]
        public void PostMoviesItemTest()
        {
            MoviesDTO movie = new MoviesDTO()
            {
                Id = 0,
                Title = "kakaos csiga",
                Length = 122,
                Description = "asd",
                Trailer = "link"
            };

            var count = _context.Movies.Count();

            var result = _controller.PostMovie(movie);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            Assert.IsAssignableFrom<MoviesDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Movies.Count());

        }
    }
}
