namespace Diving.Application.DeleteSubject;

public record DeleteSubjectCommand(long SubjectId) : ICommand;

internal class DeleteSubjectCommandHandler : ICommandHandler<DeleteSubjectCommand>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
    {
        _subjectRepository = subjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        await _subjectRepository.Delete(request.SubjectId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
