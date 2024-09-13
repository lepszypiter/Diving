namespace Diving.Domain.Models;

public interface IInstructorRepository
{
    Task<IReadOnlyCollection<Instructor>> GetAllInstructors();
    Task<Instructor?> GetById(long id);
    Task Add(Instructor instructor);
    Task Save();
    void Remove(Instructor instructor);
}
