using Diving.Application.AddInstructor;
using Diving.Application.GetInstructor;
using Diving.Application.ModifyInstructor;
using Diving.Domain.Instructor;
using Diving.Domain.Models;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstructorController : ControllerBase
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly ILogger _logger;
    private readonly ISender _sender;
    private readonly GetInstructorsQueryHandler _getInstructorsQueryHandler;
    private readonly ModifyInstructorsCommandHandler _modifyInstructorsCommandHandler;

    public InstructorController(
        IInstructorRepository instructorRepository,
        ILogger<InstructorController> logger,
        GetInstructorsQueryHandler getClientsQueryHandler,
        ModifyInstructorsCommandHandler modifyClientsCommandHandler,
        ISender sender)
    {
        _getInstructorsQueryHandler = getClientsQueryHandler;
        _modifyInstructorsCommandHandler = modifyClientsCommandHandler;
        _sender = sender;
        _instructorRepository = instructorRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
    {
        _logger.LogInformation("GET: GetAllInstructors");
        var instructors = await _getInstructorsQueryHandler.Handle();
        return Ok(instructors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Instructor>> GetInstructor(long id)
    {
        _logger.LogInformation("GET: GetInstructorWithId");
        var instructor = await _instructorRepository.GetById(id);

        return instructor ?? (ActionResult<Instructor>)NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<InstructorDto>> PostInstructor(NewInstructorDto newInstructorDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST: AddInstructor");
        var instructor = await _sender.Send(new AddInstructorCommand(newInstructorDto.Name, newInstructorDto.Surname), cancellationToken);
        return CreatedAtAction("GetInstructor", new { id = instructor.InstructorId }, instructor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutInstructor(ModifyInstructorDto dto)
    {
        _logger.LogInformation("PUT: ChangeInstructor");

        try
        {
            var result = await _modifyInstructorsCommandHandler.Handle(dto);
            return Ok(result);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInstructor(long id)
    {
        _logger.LogInformation("DELETE: DeleteInstructorWithID");
        var instructor = await _instructorRepository.GetById(id);
        if (instructor == null)
        {
            return NotFound();
        }

        _instructorRepository.Remove(instructor);
        await _instructorRepository.Save();

        return NoContent();
    }
}
