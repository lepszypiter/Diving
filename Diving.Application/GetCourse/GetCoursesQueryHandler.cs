using Diving.Domain.Models;

namespace Diving.Application.GetCourse;

public class GetCoursesQueryHandler
{
    private readonly ICourseRepository _repository;

    public GetCoursesQueryHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<CourseDto>> Handle()
    {
        var courses = await _repository.GetAllCourses();
        return courses
            .Select(x => new CourseDto(x.CourseId, x.Name, x.Instructor, x.HoursOnOpenWater, x.HoursOnPool, x.HoursOfLectures, x.Price, x.Subjects))
            .ToList();
    }
}
