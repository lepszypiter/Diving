using Diving.API.Controllers;
using Diving.Domain.Models;
using MediatR;

namespace Diving.Application.AddCourse;

internal class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, Course>
{
    private readonly ICourseRepository _courseRepository;

    public AddCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
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
        await _courseRepository.Save();
        return course;
    }
}
