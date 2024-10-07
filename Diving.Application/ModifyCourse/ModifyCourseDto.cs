namespace Diving.Application.ModifyCourse;

public record ModifyCourseDto(
    long CourseId,
    string Name,
    string? Instructor,
    int? HoursOnOpenWater,
    int? HoursOnPool,
    int? HoursOfLectures,
    decimal? Price);
