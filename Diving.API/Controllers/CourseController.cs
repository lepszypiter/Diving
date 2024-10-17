using Diving.Application.AddCourse;
using Diving.Application.DeleteCourse;
using Diving.Application.ReadCourse;
using Diving.Application.ReadCourses;
using Diving.Application.UpdateCourse;
using Diving.Domain.Course;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ISender _sender;

    public CourseController(
        ILogger<CourseController> logger,
        ISender sender)
    {
        _sender = sender;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> ReadCourses(CancellationToken cancellationToken)
    {
        _logger.LogInformation("GET: GetAllCourses");
        var courses = await  _sender.Send(new ReadCoursesQuery(), cancellationToken);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ReadCourseDto> ReadCourse(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GET: GetCourseWithId");
        return  await _sender.Send(new ReadCourseQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<ReadCoursesDto>> CreateCourse(NewCourseRequest newCourseRequest, CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST: AddCourse");
        var course = await _sender.Send(
            new AddCourseCommand(
                newCourseRequest.Name,
                newCourseRequest.Instructor,
                newCourseRequest.HoursOnOpenWater,
                newCourseRequest.HoursOnPool,
                newCourseRequest.HoursOfLectures,
                newCourseRequest.Price),
            cancellationToken);
        return CreatedAtAction("ReadCourse", new { id = course.CourseId }, course);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Course>> UpdateCourse(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PUT: ModifyCourse");
        try
        {
            var result =  await _sender.Send(
                new UpdateCourseCommand(request.CourseId, request.Name), cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DELETE: DeleteCourseWithID");
        await _sender.Send(new DeleteCourseCommand(id), cancellationToken);

        return NoContent();
    }
}
