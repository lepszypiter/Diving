namespace Diving.Application.DeleteCourse;

public record CourseDto(long Id, string Name, string Instructor, int HoursOnOpenWater, int HoursOnPool, int HoursOfLectures, decimal Price);
