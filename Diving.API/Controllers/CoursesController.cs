using Diving.Application.AddCourse;
using Diving.Application.GetCourse;
using Diving.Application.ModifyCourse;
using Diving.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger _logger;
    private readonly GetCoursesQueryHandler _getCoursesQueryHandler;
    private readonly AddCourseCommandHandler _addCourseCommandHandler;
    private readonly ModifyCourseCommandHandler _modifyCourseCommandHandler;

    public CoursesController(
        ICourseRepository courseRepository,
        ILogger<CoursesController> logger,
        GetCoursesQueryHandler getCoursesQueryHandler,
        AddCourseCommandHandler addCourseCommandHandler,
        ModifyCourseCommandHandler modifyCourseCommandHandler)
    {
        _getCoursesQueryHandler = getCoursesQueryHandler;
        _addCourseCommandHandler = addCourseCommandHandler;
        _modifyCourseCommandHandler = modifyCourseCommandHandler;
        _courseRepository = courseRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        _logger.LogInformation("GET: GetAllCourses");
        var courses = await _getCoursesQueryHandler.Handle();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(long id)
    {
        _logger.LogInformation("GET: GetCourseWithId");
        var course = await _courseRepository.GetById(id);

        return course ?? (ActionResult<Course>)NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Course>> PostCourse(NewCourseDto newCourseDto)
    {
        _logger.LogInformation("POST: AddCourse");
        var course = await _addCourseCommandHandler.Handle(newCourseDto);
        return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(ModifyCourseDto dto)
    {
        _logger.LogInformation("PUT: ModifyCourse");
        try
        {
            var result = await _modifyCourseCommandHandler.Handle(dto);
            return Ok(result);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(long id)
    {
        _logger.LogInformation("DELETE: DeleteCourseWithID");
        var course = await _courseRepository.GetById(id);
        if (course == null)
        {
            return NotFound();
        }

        _courseRepository.Remove(course);
        await _courseRepository.Save();

        return NoContent();
    }
}
