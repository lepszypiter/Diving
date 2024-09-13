namespace Diving.Domain.Models;

public interface ICourseRepository
{
    Task<IReadOnlyCollection<Course>> GetAllCourses();
    Task<Course?> GetById(long id);
    Task Add(Course course);
    Task Save();
    void Remove(Course course);
}
