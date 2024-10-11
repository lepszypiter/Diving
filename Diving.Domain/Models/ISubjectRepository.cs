using Diving.Domain.Course;

namespace Diving.Domain.Models;

public interface ISubjectRepository
{
    Task<IReadOnlyCollection<Subject?>> ReadAllSubjects(CancellationToken cancellationToken);
    Task<Subject?> GetById(long id, CancellationToken cancellationToken);
    Task Add(Subject subject);
    Task Delete(long id, CancellationToken cancellationToken);
}
