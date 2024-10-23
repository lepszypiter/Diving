using Diving.Domain.Course;

namespace Diving.Application.UpdateCourse;

public record UpdateCourseCommand(long CourseId, string Name) : ICommand<CourseDto>;

internal class UpdateCourseCommandHandler : UnitOfWorkCommandHandler<UpdateCourseCommand, CourseDto>
{
    private readonly ICourseRepository _courseRepository;

    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _courseRepository = courseRepository;
    }

    protected override async Task<CourseDto> HandleCommand(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetById(command.CourseId, cancellationToken);
        if (course is null)
        {
            throw new ArgumentException("Course does not exist");
        }

        course.ModifyCourseData(command.Name, course.Instructor, course.HoursOnOpenWater, course.HoursOnPool, course.HoursOfLectures, course.Price);
        await _courseRepository.Add(course);
        return new CourseDto(course.CourseId, course.Name, course.Instructor, course.HoursOnOpenWater, course.HoursOnPool, course.HoursOfLectures, course.Price, new List<Subject>());
    }
}
