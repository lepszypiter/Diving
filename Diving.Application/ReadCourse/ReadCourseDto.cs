using Diving.Domain.Course;

namespace Diving.Application.ReadCourse;

public record ReadCourseDto(
    long CourseId,
    string? Name,
    string? Instructor,
    int? HoursOnOpenWater,
    int? HoursOnPool,
    int? HoursOfLectures,
    decimal? Price,
    IList<Subject> Subjects);
