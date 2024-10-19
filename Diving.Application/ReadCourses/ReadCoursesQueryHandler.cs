using Diving.Domain.Course;

namespace Diving.Application.ReadCourses;

public record ReadCoursesQuery : IQuery<IReadOnlyCollection<ReadCoursesDto>>;
public class ReadCoursesQueryHandler : IQueryHandler<ReadCoursesQuery, IReadOnlyCollection<ReadCoursesDto>>
{
    private readonly ICourseRepository _repository;

    public ReadCoursesQueryHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<ReadCoursesDto>> Handle(ReadCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _repository.ReadAllCourses(cancellationToken);

        return courses
            .Select(x => new ReadCoursesDto(x.CourseId, x.Name, x.Instructor, x.HoursOnOpenWater, x.HoursOnPool, x.HoursOfLectures, x.Price, new List<Subject>(x.Subjects)))
            .ToList();
    }
}
