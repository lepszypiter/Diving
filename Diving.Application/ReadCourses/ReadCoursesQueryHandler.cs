using Diving.Domain.Course;

namespace Diving.Application.ReadCourses;

public record ReadCoursesQuery : ICommand<IReadOnlyCollection<ReadCoursesDto>>;
internal class ReadCoursesQueryHandler : UnitOfWorkCommandHandler<ReadCoursesQuery, IReadOnlyCollection<ReadCoursesDto>>
{
    private readonly ICourseRepository _repository;

    public ReadCoursesQueryHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    protected override async Task<IReadOnlyCollection<ReadCoursesDto>> HandleCommand(ReadCoursesQuery command, CancellationToken cancellationToken)
    {
        var courses = await _repository.ReadAllCourses(cancellationToken);

        return courses
            .Select(x => new ReadCoursesDto(x.CourseId, x.Name, x.Instructor, x.HoursOnOpenWater, x.HoursOnPool, x.HoursOfLectures, x.Price, new List<Subject>(x.Subjects)))
            .ToList();
    }
}
