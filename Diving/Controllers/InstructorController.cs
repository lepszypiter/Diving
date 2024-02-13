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
    public class InstructorController : ControllerBase
    {
        private readonly DivingContext _context;

        public InstructorController(DivingContext context)
        {
            _context = context;
        }

        // GET: api/Instructor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructor_1()
        {
            return await _context.Instructor_1.ToListAsync();
        }

        // GET: api/Instructor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetInstructor(long id)
        {
            var instructor = await _context.Instructor_1.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return instructor;
        }

        // PUT: api/Instructor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstructor(long id, Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return BadRequest();
            }

            _context.Entry(instructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
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

        // POST: api/Instructor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Instructor>> PostInstructor(Instructor instructor)
        {
            _context.Instructor_1.Add(instructor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstructor", new { id = instructor.InstructorId }, instructor);
        }

        // DELETE: api/Instructor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(long id)
        {
            var instructor = await _context.Instructor_1.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructor_1.Remove(instructor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstructorExists(long id)
        {
            return _context.Instructor_1.Any(e => e.InstructorId == id);
        }
    }
}
