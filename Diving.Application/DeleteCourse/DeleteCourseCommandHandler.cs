namespace Diving.Application.DeleteCourse;

public record DeleteCourseCommand(long CourseId) : ICommand;

internal class DeleteCourseCommandHandler : UnitOfWorkCommandHandler<DeleteCourseCommand>
{
    private readonly ICourseRepository _courseRepository;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _courseRepository = courseRepository;
    }

    public override async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        await _courseRepository.Delete(request.CourseId, cancellationToken);
        await base.Handle(request, cancellationToken);
    }
}
