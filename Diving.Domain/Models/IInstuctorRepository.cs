namespace Diving.Domain.Models;

public interface IInstructorRepository
{
    Task<IReadOnlyCollection<Instructor.Instructor>> GetAllInstructors();
    Task<Instructor.Instructor?> GetById(long id);
    Task Add(Instructor.Instructor instructor);
    Task Save();
    void Remove(Instructor.Instructor instructor);
}
