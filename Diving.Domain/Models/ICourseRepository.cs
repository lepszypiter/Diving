namespace Diving.Domain.Models;

public interface ICourseRepository
{
    Task<IReadOnlyCollection<Course.Course>> ReadAllCourses(CancellationToken cancellationToken);
    Task<Course.Course?> GetById(long id, CancellationToken cancellationToken);
    Task Add(Course.Course course);
    Task Save();
    Task Delete(long id, CancellationToken cancellationToken);
}
