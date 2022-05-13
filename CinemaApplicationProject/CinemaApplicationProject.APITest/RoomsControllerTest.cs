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
    public class RoomsControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly RoomsController _controller;

        public RoomsControllerTest()
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
            _controller = new RoomsController(_service);

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
        public void GetRoomsTest()
        {
            var result = _controller.GetRooms();


            var content = Assert.IsAssignableFrom<IEnumerable<RoomsDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(3, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetRoomByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetRoom(id);

            // Assert
            var content = Assert.IsAssignableFrom<RoomsDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidRoomTest()
        {
            // Arrange
            var id = 19;

            // Act
            var result = _controller.GetRoom(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void DeleteRoom()
        {
            var Count = _context.Rooms.Count();
            var result = _controller.DeleteRoom(1);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(Count - 1, _context.Rooms.Count());
        }

        [Fact]
        public void PutRoomsItemTest()
        {
            RoomsDTO room = new RoomsDTO()
            {
                Id = 1,
                Name = "perec",
                Width = 1,
                Heigth = 1
            };


            var result = _controller.PutRoom(1, room);
            Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal("perec", _context.Rooms.FirstOrDefault(b => b.Id == 1).Name);
            Assert.Equal(1, _context.Rooms.FirstOrDefault(b => b.Id == 1).Width);
        }

        [Fact]
        public void PostRoomItemTest()
        {
            RoomsDTO room = new RoomsDTO()
            {
                Id = 0,
                Name = "kakaos csiga",
                Width = 122,
                Heigth = 111,
            };

            var count = _context.Rooms.Count();

            var result = _controller.PostRoom(room);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            Assert.IsAssignableFrom<RoomsDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Rooms.Count());

        }
    }
}
