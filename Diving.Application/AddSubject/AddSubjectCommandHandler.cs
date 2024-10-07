using Diving.Application.Abstarction.Messaging;
using Diving.Domain;
using Diving.Domain.Models;

namespace Diving.Application.AddSubject;

internal class AddSubjectCommandHandler : ICommandHandler<AddSubjectCommand, Subject>
{
    private readonly ICourseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSubjectCommandHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Subject> Handle(AddSubjectCommand command, CancellationToken cancellationToken)
    {
        var course = await _repository.GetById(command.courseId);
        if (course == null)
        {
            throw new("Course not found");
        }

        var subject = Subject.CreateNewSubject(command.subjectName);

        course.Subjects.Add(Subject.CreateNewSubject(subject));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return subject;
    }
}
