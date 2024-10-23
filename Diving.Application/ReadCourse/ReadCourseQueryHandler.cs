using Diving.Domain.Course;

namespace Diving.Application.ReadCourse;

public record ReadCourseQuery(long CourseId) : ICommand<ReadCourseDto>;
internal class ReadCourseQueryHandler : UnitOfWorkCommandHandler<ReadCourseQuery, ReadCourseDto>
{
    private readonly ICourseRepository _repository;

    public ReadCourseQueryHandler(ICourseRepository repository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    protected override async Task<ReadCourseDto> HandleCommand(ReadCourseQuery command, CancellationToken cancellationToken)
    {
        var course = await _repository.GetById(command.CourseId, cancellationToken);
        if (course == null)
        {
            throw new("Course not found");
        }

        return new ReadCourseDto(course.CourseId, course.Name, course.Instructor, course.HoursOnOpenWater, course.HoursOnPool, course.HoursOfLectures, course.Price, new List<Subject>());
    }
}
