using Diving.Domain.Course;
using Diving.Domain.Models;
using MediatR;

namespace Diving.Application.AddCourse;

internal class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, Course>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Course> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Course.CreateNewCourse(
            request.Name,
            request.Instructor,
            request.HoursOnOpenWater,
            request.HoursOnPool,
            request.HoursOfLectures,
            request.Price);
        await _courseRepository.Add(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return course;
    }
}
