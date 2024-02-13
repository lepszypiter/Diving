using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Diving.Models;

namespace Diving.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientWithCourseController : ControllerBase
    {
        private readonly DivingContext _context;

        public ClientWithCourseController(DivingContext context)
        {
            _context = context;
        }

        // GET: api/ClientWithCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientWithCourse>>> GetClientWithCourses()
        {
            return await _context.ClientWithCourses.ToListAsync();
        }

        // GET: api/ClientWithCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientWithCourse>> GetClientWithCourse(long id)
        {
            var clientWithCourse = await _context.ClientWithCourses.FindAsync(id);

            if (clientWithCourse == null)
            {
                return NotFound();
            }

            return clientWithCourse;
        }

        // PUT: api/ClientWithCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientWithCourse(long id, ClientWithCourse clientWithCourse)
        {
            if (id != clientWithCourse.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientWithCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientWithCourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClientWithCourse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientWithCourse>> PostClientWithCourse(ClientWithCourse clientWithCourse)
        {
            _context.ClientWithCourses.Add(clientWithCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientWithCourse", new { id = clientWithCourse.Id }, clientWithCourse);
        }

        // DELETE: api/ClientWithCourse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientWithCourse(long id)
        {
            var clientWithCourse = await _context.ClientWithCourses.FindAsync(id);
            if (clientWithCourse == null)
            {
                return NotFound();
            }

            _context.ClientWithCourses.Remove(clientWithCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientWithCourseExists(long id)
        {
            return _context.ClientWithCourses.Any(e => e.Id == id);
        }
    }
}
