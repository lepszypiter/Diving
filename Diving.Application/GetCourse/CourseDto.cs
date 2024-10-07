using Diving.Domain.Course;

namespace Diving.Application.GetCourse;

public record CourseDto(
    long CourseId,
    string? Name,
    string? Instructor,
    int? HoursOnOpenWater,
    int? HoursOnPool,
    int? HoursOfLectures,
    decimal? Price,
    IList<Subject> Subjects);
