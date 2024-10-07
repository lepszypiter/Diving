using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Course;
using Diving.Domain.Models;

namespace Diving.Application.CreateSubject;

internal class CreateSubjectCommandHandler : ICommandHandler<CreateSubjectCommand, Subject>
{
    private readonly ICourseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubjectCommandHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Subject> Handle(CreateSubjectCommand command, CancellationToken cancellationToken)
    {
        var course = await _repository.GetById(command.CourseId);
        if (course == null)
        {
            throw new("Course not found");
        }

        var subject = Subject.CreateNewSubject(command.Name);

        course.Subjects.Add(subject);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return subject;
    }
}
