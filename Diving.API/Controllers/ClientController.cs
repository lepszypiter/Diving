using Diving.Application.AddClient;
using Diving.Application.GetClients;
using Diving.Application.ModifyClients;
using Diving.Domain.Clients;
using Diving.Domain.Models;
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
    private readonly ModifyClientsCommandHandler _modifyClientsCommandHandler;

    public ClientController(
        IClientRepository clientRepository,
        ILogger<ClientController> logger,
        GetClientsQueryHandler getClientsQueryHandler,
        AddClientCommandHandler addClientCommandHandler,
        ModifyClientsCommandHandler modifyClientsCommandHandler)
    {
        _getClientsQueryHandler = getClientsQueryHandler;
        _addClientCommandHandler = addClientCommandHandler;
        _modifyClientsCommandHandler = modifyClientsCommandHandler;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(long id)
    {
        _logger.LogInformation("GET: GetClientWithId");
        var client = await _clientRepository.GetById(id);//_context.Clients.FindAsync(id);

        return client ?? (ActionResult<Client>)NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> PostClient(NewClientDto newClientDto)
    {
        _logger.LogInformation("POST: AddClient");
        var client = await _addClientCommandHandler.Handle(newClientDto);
        return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> PutClient(ModifyClientDto dto)
    {
        _logger.LogInformation("PUT: ChangeClient");

        try
        {
           var result =  await _modifyClientsCommandHandler.Handle(dto);
           return Ok(result);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
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
