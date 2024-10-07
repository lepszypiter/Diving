using Diving.Domain.Course;
using Diving.Domain.Models;

namespace Diving.Application.ModifyCourse;

public class ModifyCoursesCommandHandler
{
    private readonly ICourseRepository _courseRepository;

    public ModifyCoursesCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Course> Handle(ModifyCourseDto modifyCourseDto)
    {
        var course = await _courseRepository.GetById(modifyCourseDto.CourseId);
        if (course is null)
        {
            throw new ArgumentException("Course does not exist");
        }

        course.ModifyCourseData(
            modifyCourseDto.Name,
            modifyCourseDto.Instructor!,
            modifyCourseDto.HoursOnOpenWater,
            modifyCourseDto.HoursOnPool,
            modifyCourseDto.HoursOfLectures,
            modifyCourseDto.Price);
        await _courseRepository.Save();

        return course;
    }
}
