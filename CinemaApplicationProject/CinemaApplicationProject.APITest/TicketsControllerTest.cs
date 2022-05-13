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
    public class TicketsControllerTest : IDisposable
    {

        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly TicketsController _controller;

        public TicketsControllerTest()
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
            _controller = new TicketsController(_service);

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
        public void GetTicketsTest()
        {
            var result = _controller.GetTickets();


            var content = Assert.IsAssignableFrom<IEnumerable<TicketsDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(3, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetTicketByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetTicketById(id);

            // Assert
            var content = Assert.IsAssignableFrom<TicketsDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void PutTicketItemTest()
        {
            TicketsDTO movie = new TicketsDTO()
            {
                Id = 1,
                Type = "perec",
                Price = 1
            };


            var result = _controller.PutTicket(1, movie);
            Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal("perec", _context.Tickets.FirstOrDefault(b => b.Id == 1).Type);
            Assert.Equal(1, _context.Tickets.FirstOrDefault(b => b.Id == 1).Price);
        }

        [Fact]
        public void PostTicketTest()
        {
            TicketsDTO ticket = new TicketsDTO()
            {
                Id = 0,
                Type = "kakaos csiga",
                Price = 122
            };

            var count = _context.Tickets.Count();

            var result = _controller.PostTicket(ticket);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            Assert.IsAssignableFrom<TicketsDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Tickets.Count());
        }

        [Fact]
        public void DeleteTicketTest()
        {
            var mCount = _context.Tickets.Count();
            var result = _controller.DeleteTickets(1);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(mCount - 1, _context.Tickets.Count());
        }
    }
}
