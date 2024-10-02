using Diving.Application.AddClient;
using Diving.Application.GetClients;
using Diving.Application.ModifyClients;
using Diving.Domain.Clients;
using Diving.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diving.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger _logger;
    private readonly ISender _sender;
    internal readonly GetClientsQueryHandler _getClientsQueryHandler;
    internal readonly ModifyClientsCommandHandler _modifyClientsCommandHandler;

    internal ClientController(
        IClientRepository clientRepository,
        ILogger<ClientController> logger,
        GetClientsQueryHandler getClientsQueryHandler,
        ModifyClientsCommandHandler modifyClientsCommandHandler,
        ISender sender)
    {
        _getClientsQueryHandler = getClientsQueryHandler;
        _modifyClientsCommandHandler = modifyClientsCommandHandler;
        _sender = sender;
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
    public async Task<ActionResult<ClientDto>> PostClient(NewClientDto newClientDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST: AddClient");
        var client = await _sender.Send(new AddClientCommand(newClientDto.Name, newClientDto.Surname, newClientDto.Email), cancellationToken);

        return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> PutClient(ModifyClientDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PUT: ChangeClient");

        try
        {
           var result =  await _modifyClientsCommandHandler.Handle(dto, cancellationToken);
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
        //await _clientRepository.Save();

        return NoContent();
    }
}
