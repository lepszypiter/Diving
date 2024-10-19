namespace Diving.Application.ReadSubjects;

public record ReadSubjectsQuery(int CourseId) : IQuery<IReadOnlyCollection<SubjectDto>>;

internal class ReadSubjectsQueryHandler : IQueryHandler<ReadSubjectsQuery, IReadOnlyCollection<SubjectDto>>
{
    private readonly ICourseRepository _repository;

    public ReadSubjectsQueryHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<SubjectDto>> Handle(ReadSubjectsQuery query, CancellationToken cancellationToken)
    {
        var subjects = await _repository.GetById(query.CourseId, cancellationToken);
        if (subjects == null)
        {
            throw new("Course not found");
        }

        return subjects.Subjects
            .Select(x => new SubjectDto(x.SubjectId, x.Name))
            .ToList();
    }
}
