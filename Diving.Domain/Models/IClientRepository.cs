using Diving.Domain.Clients;

namespace Diving.Domain.Models;

public interface IClientRepository
{
    Task<IReadOnlyCollection<Client>> GetAllClients();
    Task<Client?> GetById(long id);
    Task Add(Client client);
    void Remove(Client client);
}
