using Diving.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diving.Repositories;

public interface IClientRepository
{
    Task<IReadOnlyCollection<Client>> GetAllClients();
    Task<Client?> GetById(long id);
    Task Add(Client client);
    Task Save();
    void Remove(Client client);
}