using Diving.Domain.Models;
using Diving.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientWithCourseController : ControllerBase
{
    private readonly DivingContext _context;

    public ClientWithCourseController(DivingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientWithCourse>>> GetClientWithCourses()
    {
        return await _context.ClientWithCourses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientWithCourse>> GetClientWithCourse(long id)
    {
        var clientWithCourse = await _context.ClientWithCourses.FindAsync(id);

        return clientWithCourse ?? (ActionResult<ClientWithCourse>)NotFound();
    }

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
        catch (DbUpdateConcurrencyException) when (!ClientWithCourseExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ClientWithCourse>> PostClientWithCourse(ClientWithCourse clientWithCourse)
    {
        _context.ClientWithCourses.Add(clientWithCourse);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetClientWithCourse", new { id = clientWithCourse.Id }, clientWithCourse);
    }

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
