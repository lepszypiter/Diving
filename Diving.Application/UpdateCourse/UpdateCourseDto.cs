using Diving.Domain.Course;

namespace Diving.Application.UpdateCourse;

public record CourseDto(long CourseId, string Name, string Instructor, int HoursOnOpenWater, int HoursOnPool, int HoursOfLectures, decimal Price, List<Subject> Subjects);
