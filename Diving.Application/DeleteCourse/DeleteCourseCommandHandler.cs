namespace Diving.Application.DeleteCourse;

public record DeleteCourseCommand(long CourseId) : ICommand;

internal class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        await _courseRepository.Delete(request.CourseId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
