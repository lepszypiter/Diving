namespace Diving.Application.ReadSubjects;

public record ReadSubjectsQuery(int CourseId) : ICommand<IReadOnlyCollection<SubjectDto>>;

internal class ReadSubjectsQueryHandler : UnitOfWorkCommandHandler<ReadSubjectsQuery, IReadOnlyCollection<SubjectDto>>
{
    private readonly ICourseRepository _repository;

    public ReadSubjectsQueryHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    protected override async Task<IReadOnlyCollection<SubjectDto>> HandleCommand(ReadSubjectsQuery command, CancellationToken cancellationToken)
    {
        var subjects = await _repository.GetById(command.CourseId, cancellationToken);
        if (subjects == null)
        {
            throw new("Course not found");
        }

        return subjects.Subjects
            .Select(x => new SubjectDto(x.SubjectId, x.Name))
            .ToList();
    }
}
