using Diving.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diving.Infrastructure.Repositories;

internal class ClientRepository : IClientRepository
{
    private readonly DivingContext _context;
    private readonly ILogger _logger;

    public ClientRepository(DivingContext context, ILogger<ClientRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Client>> GetAllClients()
    {
        var result = await _context.Clients.Take(15).ToListAsync();
        _logger.LogTrace("GetAllClients {Count}",  result.Count);
        return result;
    }

    public async Task<Client?> GetById(long id)
    {
        _logger.LogInformation("Id {}", id);// log id 
        return await _context.Clients.FindAsync(id);
    }

    public async Task Add(Client client)
    {
        await _context.Clients.AddAsync(client);

    }

    public async Task Save()
    {
        var ret = await _context.SaveChangesAsync();
        _logger.LogInformation("Record changed {Count}", ret);// log changed record 
    }

    public void Remove(Client client)
    {
        _context.Clients.Remove(client);
    }
}