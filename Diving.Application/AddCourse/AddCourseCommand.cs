using Diving.Domain.Course;

namespace Diving.Application.AddCourse;

public record AddCourseCommand(string Name, string Instructor, int HoursOnOpenWater, int HoursOnPool, int HoursOfLectures, decimal Price) : ICommand<Course>;
