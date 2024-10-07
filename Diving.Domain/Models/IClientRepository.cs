namespace Diving.Domain.Models;

public interface IClientRepository
{
    Task<IReadOnlyCollection<Client.Client>> ReadAllClients(CancellationToken cancellationToken);
    Task<Client.Client?> GetById(long id, CancellationToken cancellationToken);
    Task Add(Client.Client client);
    Task Delete(long id, CancellationToken cancellationTokent);
}
