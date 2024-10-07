using System.Diagnostics.CodeAnalysis;
using Diving.Application.CreateSubject;
using Diving.Application.ReadSubjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable UnusedParameter.Global
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Diving.API.Controllers;

public record AddSubjectDto (string Name);

public record SubjectDto (int Id, string Name);

[Route("api/course")]
[ApiController]
[SuppressMessage("Roslynator", "RCS1163:Unused parameter")]
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
    public async Task<ActionResult<IEnumerable<SubjectDto>>> ReadSubjects(int courseId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("READ: ReadSubjects");
        var subjects = await _sender.Send(new ReadSubjectsQuery(courseId), cancellationToken);
        return Ok(subjects);
    }

    [HttpPost("{courseId}/subjects")]
    public async Task<ActionResult<SubjectDto>> CreateSubject(int courseId, AddSubjectDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST: CreateSubject");
        var subject = await _sender.Send(new CreateSubjectCommand(courseId, dto.Name), cancellationToken);
        return CreatedAtAction("CreateSubject", new { courseId, id = subject.SubjectId }, subject);
    }

    [HttpPut("{courseId}/subjects/{id}")]
    public async Task<ActionResult<SubjectDto>> UpdateSubjects(int courseId, int id,  SubjectDto dto)
    {
        return Ok();
    }

    [HttpDelete("{courseId}/subjects/{id}")]
    public async Task<ActionResult<SubjectDto>> DeleteSubject(int courseId, int id)
    {
        return Ok();
    }
}
