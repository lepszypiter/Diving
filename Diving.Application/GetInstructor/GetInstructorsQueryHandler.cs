using Diving.Domain.Models;

namespace Diving.Application.GetInstructor;

public class GetInstructorsQueryHandler
{
    private readonly IInstructorRepository _instructorRepository;

    public GetInstructorsQueryHandler(IInstructorRepository instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }

    public async Task<IReadOnlyCollection<InstructorDto>> Handle()
    {
        var instructors = await _instructorRepository.GetAllInstructors();

        return instructors
            .Select(x => new InstructorDto(x.InstructorId, x.Name, x.Surname))
            .ToList();
    }
}
