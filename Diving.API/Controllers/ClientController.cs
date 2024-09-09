using Diving.Application.AddClient;
using Diving.Application.GetClients;
using Diving.Domain.Models;
using Diving.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger _logger;
    private readonly GetClientsQueryHandler _getClientsQueryHandler;
    private readonly AddClientCommandHandler _addClientCommandHandler;

    public ClientController(
        IClientRepository clientRepository,
        ILogger<ClientController> logger,
        GetClientsQueryHandler getClientsQueryHandler,
        AddClientCommandHandler addClientCommandHandler)
    {
        _getClientsQueryHandler = getClientsQueryHandler;
        _addClientCommandHandler = addClientCommandHandler;
        _clientRepository = clientRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
    {
        _logger.LogInformation("GET: GetAllClients");
        var clients = await _getClientsQueryHandler.Handle();
        return Ok(clients);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> PostClient(NewClientDto newClientDto)
    {
        _logger.LogInformation("POST: AddClient");
        var client = await _addClientCommandHandler.Handle(newClientDto);
        return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(long id)
    {
        _logger.LogInformation("GET: GetClientWithId");
        var client = await _clientRepository.GetById(id);//_context.Clients.FindAsync(id);

        return client ?? (ActionResult<Client>)NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(long id, Client client)
    {
        _logger.LogInformation("PUT: AddOrChangeClient");
        if (id != client.ClientId)
        {
            return BadRequest();
        }

        await _clientRepository.Add(client);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(long id)
    {
        _logger.LogInformation("DELETE: DeleteClientWithID");
        var client = await _clientRepository.GetById(id);
        if (client == null)
        {
            return NotFound();
        }

        _clientRepository.Remove(client);
        await _clientRepository.Save();

        return NoContent();
    }
}
