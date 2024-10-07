using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable UnusedParameter.Global
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Diving.API.Controllers;

[Route("api/course")]
[ApiController]
[SuppressMessage("Roslynator", "RCS1163:Unused parameter")]
public class SubjectController : ControllerBase
{
    [HttpGet("{courseId}/subjects")]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> ReadSubjects(int courseId)
    {
        return Ok(Array.Empty<SubjectDto>());
    }

    [HttpPost("{courseId}/subjects")]
    public async Task<ActionResult<SubjectDto>> AddSubjects(int courseId, AddSubjectDto dto)
    {
        return Ok();
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

public record AddSubjectDto
{
}

public record SubjectDto
{
}
