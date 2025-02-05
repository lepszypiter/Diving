﻿using Diving.Domain.Client;
using Diving.Domain.Models;
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

    public async Task<IReadOnlyCollection<Client>> ReadAllClients(CancellationToken cancellationToken)
    {
        var result = await _context.Clients.ToListAsync(cancellationToken);
        _logger.LogTrace("GetAllClients {Count}",  result.Count);
        return result;
    }

    public async Task<Client?> GetById(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Id {}", id);// log id
        return await _context.Clients.FindAsync([ id ], cancellationToken);
    }

    public async Task Add(Client client)
    {
        await _context.Clients.AddAsync(client);
    }

    public Task Delete(long id, CancellationToken cancellationToken)
    {
        _context.Clients.Remove(_context.Clients.Single(x => x.ClientId == id));
        return Task.CompletedTask;
    }

    public async Task Save()
    {
        var ret = await _context.SaveChangesAsync();
        _logger.LogInformation("Record changed {Count}", ret);// log changed record
    }
}
