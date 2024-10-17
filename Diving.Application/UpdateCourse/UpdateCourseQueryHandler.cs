using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Course;
using Diving.Domain.Models;

namespace Diving.Application.UpdateCourse;

public record UpdateCourseCommand(long CourseId, string Name) : ICommand<CourseDto>;

internal class UpdateCourseCommandHandler : ICommandHandler<UpdateCourseCommand, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CourseDto> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetById(command.CourseId, cancellationToken);
        if (course is null)
        {
            throw new ArgumentException("Course does not exist");
        }

        course.ModifyCourseData(command.Name, course.Instructor, course.HoursOnOpenWater, course.HoursOnPool, course.HoursOfLectures, course.Price);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new CourseDto(course.CourseId, course.Name, course.Instructor, course.HoursOnOpenWater, course.HoursOnPool, course.HoursOfLectures, course.Price, new List<Subject>());
    }
}
