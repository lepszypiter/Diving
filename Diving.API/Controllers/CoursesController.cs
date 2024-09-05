using Diving.Infrastructure;
using Diving.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly DivingContext _context;

    public CoursesController(DivingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(long id)
    {
        var course = await _context.Courses.FindAsync(id);

        return course ?? (ActionResult<Course>)NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(long id, Course course)
    {
        if (id != course.CourseId)
        {
            return BadRequest();
        }

        _context.Entry(course).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!CourseExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Course>> PostCourse(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(long id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CourseExists(long id)
    {
        return _context.Courses.Any(e => e.CourseId == id);
    }
}
