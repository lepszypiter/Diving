using Diving.Models;
using Microsoft.EntityFrameworkCore;

namespace Diving.Repositories;

class ClientRepository : IClientRepository
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
        _context.Entry(client).State = EntityState.Modified;

        try
        {
            await Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "AddClient ClientNotFound {Msg}", ex.Message);// log execption as error 
            // if (!ClientExists(id))
            // {
            //     return NotFound();
            // }
            // else
            {
                throw;
            }
        }

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