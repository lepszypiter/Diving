using Diving.Application.AddCourse;
using Diving.Application.GetCourse;
using Diving.Application.ModifyCourse;
using Diving.Domain.Course;
using Diving.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger _logger;
    private readonly ISender _sender;
    private readonly GetCoursesQueryHandler _getCoursesQueryHandler;
    private readonly ModifyCoursesCommandHandler _modifyCoursesCommandHandler;

    public CourseController(
        ICourseRepository courseRepository,
        ILogger<CourseController> logger,
        GetCoursesQueryHandler getCoursesQueryHandler,
        ModifyCoursesCommandHandler modifyCoursesCommandHandler,
        ISender sender)
    {
        _getCoursesQueryHandler = getCoursesQueryHandler;
        _modifyCoursesCommandHandler = modifyCoursesCommandHandler;
        _sender = sender;
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
    public async Task<ActionResult<CourseDto>> PostCourse(NewCourseDto newCourseDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST: AddCourse");
        var course = await _sender.Send(
            new AddCourseCommand(newCourseDto.Name, newCourseDto.Instructor, newCourseDto.HoursOnOpenWater, newCourseDto.HoursOnPool, newCourseDto.HoursOfLectures, newCourseDto.Price),
            cancellationToken);
        return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(ModifyCourseDto dto)
    {
        _logger.LogInformation("PUT: ModifyCourse");
        try
        {
            var result = await _modifyCoursesCommandHandler.Handle(dto);
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
