﻿using System;
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
    public class TicketsController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public TicketsController(IDatabaseService service)
        {
            _service = service;
            DatabaseManipulation.context = _service.GetContext();
        }

        // GET: api/Tickets
        [HttpGet]
        public ActionResult<IEnumerable<TicketsDTO>> GetTickets()
        {
            return _service.GetTickets().Select(t => (TicketsDTO)t).ToList();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public ActionResult<TicketsDTO> GetTicketById(int id)
        {
            var ticket = _service.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return (TicketsDTO)ticket;
        }

        //PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutTickets(int id, TicketsDTO tickets)
        {
            if (id != tickets.Id)
            {
                return BadRequest();
            }
            var ticket = _service.GetTicketById(tickets.Id);
            ticket.Price = tickets.Price;
            ticket.Type = tickets.Type;
            if (DatabaseManipulation.UpdateElementAsync(ticket))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Tickets> PostTickets(TicketsDTO tickets)
        {
            var ticket = DatabaseManipulation.AddElement((Tickets)tickets);

            if (ticket == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, (TicketsDTO)ticket);
            }
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTickets(int id)
        {
            var ticket = _service.GetTicketById(id);
            var delete = DatabaseManipulation.DeleteElement(ticket);
            if (!delete)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
