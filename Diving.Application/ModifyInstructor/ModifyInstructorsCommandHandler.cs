using Diving.Domain.Models;

namespace Diving.Application.ModifyInstructor;

public class ModifyInstructorsCommandHandler
{
    private readonly IInstructorRepository _instructorRepository;

    public ModifyInstructorsCommandHandler(IInstructorRepository instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }

    public async Task<Instructor> Handle(ModifyInstructorDto modifyInstructorDto)
    {
        var instructor = await _instructorRepository.GetById(modifyInstructorDto.InstructorId);
        if (instructor is null)
        {
            throw new ArgumentException("Instructor does not exist");
        }

        instructor.ModifyInstructorData(modifyInstructorDto.Name, modifyInstructorDto.Surname);
        await _instructorRepository.Save();

        return instructor;
    }
}
