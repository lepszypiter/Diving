using Diving.Application.CreateSubject;
using Diving.Application.DeleteSubject;
using Diving.Application.ReadSubjects;

namespace Diving.API.Controllers;

public record AddSubjectDto (string Name);

public record SubjectRequest (int Id, string Name);

[Route("api/course")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ISender _sender;

    public SubjectController(ILogger<SubjectController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("{courseId}/subjects")]
    public async Task<ActionResult<IEnumerable<SubjectRequest>>> ReadSubjects(int courseId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("READ: ReadSubjects");
        var subjects = await _sender.Send(new ReadSubjectsQuery(courseId), cancellationToken);
        return Ok(subjects);
    }

    [HttpPost("{courseId}/subjects")]
    public async Task<ActionResult<SubjectRequest>> CreateSubject(int courseId, AddSubjectDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST: CreateSubject");
        var subject = await _sender.Send(new CreateSubjectCommand(courseId, dto.Name), cancellationToken);
        return CreatedAtAction("CreateSubject", new { courseId, id = subject.SubjectId }, subject);
    }

    [HttpPut("{courseId}/subjects/{id}")]
    public async Task<ActionResult<SubjectRequest>> UpdateSubjects(SubjectRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PUT: ChangeClient");

        try
        {
            var result =  await _sender.Send(new SubjectRequest(request.Id, request.Name), cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{courseId}/subjects/{id}")]
    public async Task<ActionResult<SubjectRequest>> DeleteSubject(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DELETE: DeleteSubjectWithID");
        await _sender.Send(new DeleteSubjectCommand(id), cancellationToken);

        return NoContent();
    }
}
