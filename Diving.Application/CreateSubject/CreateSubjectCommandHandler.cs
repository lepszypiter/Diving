using Diving.Domain.Course;

namespace Diving.Application.CreateSubject;

internal class CreateSubjectCommandHandler : UnitOfWorkCommandHandler<CreateSubjectCommand, Subject>
{
    private readonly ICourseRepository _repository;

    public CreateSubjectCommandHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    protected override async Task<Subject> HandleCommand(CreateSubjectCommand command, CancellationToken cancellationToken)
    {
        var course = await _repository.GetById(command.CourseId, cancellationToken);
        if (course == null)
        {
            throw new("Course not found");
        }

        var subject = Subject.CreateNewSubject(command.Name);

        course.Subjects.Add(subject);

        return subject;
    }
}
