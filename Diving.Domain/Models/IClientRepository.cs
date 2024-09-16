using Diving.Domain.Clients;

namespace Diving.Domain.Models;

public interface IClientRepository
{
    Task<IReadOnlyCollection<Client>> GetAllClients();
    Task<Client?> GetById(long id);
    Task Add(Client client);
    Task Save();
    void Remove(Client client);
}
