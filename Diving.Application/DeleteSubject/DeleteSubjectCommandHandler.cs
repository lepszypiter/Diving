namespace Diving.Application.DeleteSubject;

public record DeleteSubjectCommand(long SubjectId) : ICommand;

internal class DeleteSubjectCommandHandler : UnitOfWorkCommandHandler<DeleteSubjectCommand>
{
    private readonly ISubjectRepository _subjectRepository;

    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _subjectRepository = subjectRepository;
    }

    public override async Task Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        await _subjectRepository.Delete(request.SubjectId, cancellationToken);
        await base.Handle(request, cancellationToken);
    }
}
