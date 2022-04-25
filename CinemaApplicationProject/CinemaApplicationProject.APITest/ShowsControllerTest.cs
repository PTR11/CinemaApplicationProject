using System;
using System.Collections.Generic;
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
    public class ShowsControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly ShowsController _controller;

        public ShowsControllerTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DatabaseContext(options);
            TestDbInitializer.Initialize(_context);

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser, StatsAndPays, DatabaseContext, int>(_context), null,
                new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null);

            var user = new ApplicationUser { UserName = "testName", Id = 1 };
            userManager.CreateAsync(user, "testPassword").Wait();

            _service = new DatabaseService(_context, userManager);
            _controller = new ShowsController(_service);

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
        public void GetShowsTest()
        {
            var result = _controller.GetShows();


            var content = Assert.IsAssignableFrom<IEnumerable<ShowsDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(3, content.Count());
        }

        [Fact]
        public void GetShowsByDateTest()
        {
            var result = _controller.GetShowsByDate(DateTime.Now.ToString("yyyy.MM.dd"));


            var content = Assert.IsAssignableFrom<IEnumerable<MoviesDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(1, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetMovieByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetShowById(id);

            // Assert
            var content = Assert.IsAssignableFrom<ShowsDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetAvailableDatesTest()
        {
            var result = _controller.GetAvailableDates();


            var content = Assert.IsAssignableFrom<IEnumerable<DateTime>>(result.Value);

            Assert.Equal(2, content.Count());
            Assert.Contains(DateTime.Now.Date, content.Select(m => m.Date));
        }

        [Fact]
        public void GetTodayShowsTest()
        {
            var result = _controller.GetTodaysShows();


            var content = Assert.IsAssignableFrom<IEnumerable<ShowsDTO>>(result.Value);

            Assert.Equal(1, content.Count());
            Assert.Contains(DateTime.Now.Date, content.Select(m => m.Date.Date).ToList());
        }

        [Fact]
        public void PutShowsItemTest()
        {
            ShowsDTO show = new ShowsDTO()
            {
                Id = 3,
                MovieId = 5,
                RoomId = 3
            };


            var result = _controller.PutShows(3, show);
            Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(5, _context.Shows.FirstOrDefault(b => b.Id == 3).MovieId);
            Assert.Equal(3, _context.Shows.FirstOrDefault(b => b.Id == 3).RoomId);
        }

        [Fact]
        public void DeleteShowTest()
        {
            var mCount = _context.Shows.Count();
            var result = _controller.DeleteShows(1);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(mCount - 1, _context.Shows.Count());
        }

        [Fact]
        public void PostShowsItemTest()
        {
            ShowsDTO movie = new ShowsDTO()
            {
                Id = 0,
                MovieId = 1,
                RoomId = 3,
                Date = DateTime.Now.AddDays(2).AddHours(3),
            };

            var count = _context.Shows.Count();

            var result = _controller.PostShows(movie);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            Assert.IsAssignableFrom<ShowsDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Shows.Count());

        }
    }
}
