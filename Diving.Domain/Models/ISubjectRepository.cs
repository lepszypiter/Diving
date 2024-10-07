using Diving.Domain.Clients;

namespace Diving.Domain.Models;

public interface ISubjectRepository
{
    Task<IReadOnlyCollection<Client>> GetAllSubjects();
    Task<Client?> GetById(long id);
    Task Add(Subject subject);
    Task Save();
    void Remove(Subject subject);
}
