namespace Diving.Application.AddCourse;

public record NewCourseRequest(string Name, string Instructor, int HoursOnOpenWater, int HoursOnPool, int HoursOfLectures, decimal Price);
