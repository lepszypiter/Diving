using Diving.Domain.Course;

namespace Diving.Application.ReadCourse;

public record ReadCourseQuery(long CourseId) : IQuery<ReadCourseDto>;
internal class ReadCourseQueryHandler : IQueryHandler<ReadCourseQuery, ReadCourseDto>
{
    private readonly ICourseRepository _repository;

    public ReadCourseQueryHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReadCourseDto> Handle(ReadCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetById(request.CourseId, cancellationToken);
        if (course == null)
        {
            throw new("Course not found");
        }

        return new ReadCourseDto(course.CourseId, course.Name, course.Instructor, course.HoursOnOpenWater, course.HoursOnPool, course.HoursOfLectures, course.Price, new List<Subject>());
    }
}
