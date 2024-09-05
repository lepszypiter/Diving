using Diving.Infrastructure;
using Diving.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstructorController : ControllerBase
{
    private readonly DivingContext _context;

    public InstructorController(DivingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
    {
        return await _context.Instructors.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Instructor>> GetInstructor(long id)
    {
        var instructor = await _context.Instructors.FindAsync(id);

        return instructor ?? (ActionResult<Instructor>)NotFound();
    }

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
        catch (DbUpdateConcurrencyException) when (!InstructorExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Instructor>> PostInstructor(Instructor instructor)
    {
        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetInstructor", new { id = instructor.InstructorId }, instructor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInstructor(long id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null)
        {
            return NotFound();
        }

        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool InstructorExists(long id)
    {
        return _context.Instructors.Any(e => e.InstructorId == id);
    }
}
