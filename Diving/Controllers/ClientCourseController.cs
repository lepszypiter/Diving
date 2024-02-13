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
    public class ClientCourseController : ControllerBase
    {
        private readonly DivingContext _context;

        public ClientCourseController(DivingContext context)
        {
            _context = context;
        }

        // GET: api/ClientCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientCourse>>> GetClientCourses()
        {
            return await _context.ClientCourses.ToListAsync();
        }

        // GET: api/ClientCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientCourse>> GetClientCourse(long id)
        {
            var clientCourse = await _context.ClientCourses.FindAsync(id);

            if (clientCourse == null)
            {
                return NotFound();
            }

            return clientCourse;
        }

        // PUT: api/ClientCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientCourse(long id, ClientCourse clientCourse)
        {
            if (id != clientCourse.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientCourseExists(id))
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

        // POST: api/ClientCourse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientCourse>> PostClientCourse(ClientCourse clientCourse)
        {
            _context.ClientCourses.Add(clientCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientCourse", new { id = clientCourse.Id }, clientCourse);
        }

        // DELETE: api/ClientCourse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientCourse(long id)
        {
            var clientCourse = await _context.ClientCourses.FindAsync(id);
            if (clientCourse == null)
            {
                return NotFound();
            }

            _context.ClientCourses.Remove(clientCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientCourseExists(long id)
        {
            return _context.ClientCourses.Any(e => e.Id == id);
        }
    }
}
