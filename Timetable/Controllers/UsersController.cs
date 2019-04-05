﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timetable.Data;
using Timetable.Models;

namespace Timetable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        } 

        // PUT: api/Users/5
        [HttpPut]
        public IActionResult PutUser([FromBody] User user)
        {            
            if (user==null)
            {
                return BadRequest();
            }

            if(!_context.Users.Any(x=>x.Id==user.Id))
            {
                return NotFound();
            }

            _context.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user==null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}