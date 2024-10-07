namespace Diving.Domain.Models;

public interface ICourseRepository
{
    Task<IReadOnlyCollection<Course.Course>> GetAllCourses();
    Task<Course.Course?> GetById(long id);
    Task Add(Course.Course course);
    Task Save();
    void Remove(Course.Course course);
}
