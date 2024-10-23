using Diving.Domain.Course;

namespace Diving.Application.AddCourse;

internal class AddCourseCommandHandler : UnitOfWorkCommandHandler<AddCourseCommand, Course>
{
    private readonly ICourseRepository _courseRepository;

    public AddCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _courseRepository = courseRepository;
    }

    protected override async Task<Course> HandleCommand(AddCourseCommand command, CancellationToken cancellationToken)
    {
        var course = Course.CreateNewCourse(
            command.Name,
            command.Instructor,
            command.HoursOnOpenWater,
            command.HoursOnPool,
            command.HoursOfLectures,
            command.Price);
        await _courseRepository.Add(course);
        return course;
    }
}
