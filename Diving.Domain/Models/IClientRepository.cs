namespace Diving.Domain.Models;

public interface IClientRepository
{
    Task<IReadOnlyCollection<Client.Client>> GetAllClients();
    Task<Client.Client?> GetById(long id);
    Task Add(Client.Client client);
    void Remove(Client.Client client);
}
