using Diving.Domain.Course;

namespace Diving.Application.ReadCourses;

public record ReadCoursesDto(
    long CourseId,
    string? Name,
    string? Instructor,
    int? HoursOnOpenWater,
    int? HoursOnPool,
    int? HoursOfLectures,
    decimal? Price,
    IList<Subject> Subjects);
