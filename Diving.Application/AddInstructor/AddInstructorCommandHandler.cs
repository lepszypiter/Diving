using Diving.Domain.Instructor;

namespace Diving.Application.AddInstructor;

internal class AddInstructorCommandHandler : ICommandHandler<AddInstructorCommand, Instructor>
{
    private readonly IInstructorRepository _instructorRepository;

    public AddInstructorCommandHandler(IInstructorRepository instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }

    public async Task<Instructor> Handle(AddInstructorCommand newInstructorDto, CancellationToken cancellationToken)
    {
        var instructor = Instructor.CreateNewInstructor(newInstructorDto.Name, newInstructorDto.Surname);

            await _instructorRepository.Add(instructor);
            await _instructorRepository.Save();
            return instructor;
    }
}
