using Diving.Models;
using Microsoft.EntityFrameworkCore;

namespace Diving.Repositories;

class ClientRepository : IClientRepository
{
    private readonly DivingContext _context;

    public ClientRepository(DivingContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Client>> GetAllClients()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetById(long id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task Add(Client client)
    {
        _context.Entry(client).State = EntityState.Modified;

        try
        {
            await Save();
        }
        catch (DbUpdateConcurrencyException)
        {
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
        await _context.SaveChangesAsync();
    }

    public void Remove(Client client)
    {
        _context.Clients.Remove(client);
    }
}