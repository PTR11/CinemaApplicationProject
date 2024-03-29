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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CinemaApplicationProject.Model.DTOs;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionsController : ControllerBase
    {
        private readonly IDatabaseService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public OpinionsController(IDatabaseService service, UserManager<ApplicationUser> userManager)
        {
           _service = service;
            _userManager = userManager;
        }

        // GET: api/Opinions
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<OpinionsDTO>> GetOpinions(int id)
        {

            return _service.GetAllOpinionsByMovie(id).Select(o => (OpinionsDTO)o).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<OpinionsDTO>> PostOpinion(OpinionsDTO opinion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }
            else
            {
                ApplicationUser user = null;
                if (User.Identity.IsAuthenticated)
                {
                    user = await _userManager.FindByNameAsync(User.Identity.Name);
                }else if(opinion.GuestId != 0 && user == null)
                {
                    user = await _userManager.FindByIdAsync(opinion.GuestId+"");
                }
                else
                {
                    ModelState.AddModelError("", "You need to login for add opinion places!");
                    return BadRequest(ModelState);
                }
                
                if (!await _service.SaveOpinionAsync(opinion))
                {
                    ModelState.AddModelError("", "Something went wrong with the process");
                    return BadRequest(ModelState);
                }
                return Ok("Successfull reservation");
            }
        }
    }
}
