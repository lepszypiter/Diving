namespace Diving.Application.AddCourse;

public record NewCourseDto(string Name, string Instructor, int HoursOnOpenWater, int HoursOnPool, int HoursOfLectures, decimal Price);
