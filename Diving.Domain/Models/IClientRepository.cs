namespace Diving.Domain.Models;

public interface IClientRepository
{
    Task<IReadOnlyCollection<Client.Client>> ReadAllClients(CancellationToken cancellationToken);
    Task<Client.Client?> GetById(long id, CancellationToken cancellationToken);
    Task Add(Client.Client client);
    void Remove(Client.Client client);
}
