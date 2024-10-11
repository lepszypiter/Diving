using Diving.Application.AddClient;
using Diving.Application.DeleteClient;
using Diving.Application.ReadClients;
using Diving.Application.UpdateClient;
using Diving.Domain.Client;
using Diving.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diving.API.Controllers;
public record UpdateClientRequest(long ClientId, string Name, string Surname);

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger _logger;
    private readonly ISender _sender;

    public ClientController(
        IClientRepository clientRepository,
        ILogger<ClientController> logger,
        ISender sender)
    {
        _sender = sender;
        _clientRepository = clientRepository;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ReadClientsDto>>> ReadClients(CancellationToken cancellationToken)
    {
        _logger.LogInformation("GET: GetAllClients");
        var clients = await _sender.Send(new ReadClientsQuery(), cancellationToken);
        return Ok(clients);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<Application.ReadClient.ReadClientDto> ReadClient(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GET: GetClientWithId");
        return  await _sender.Send(new Application.ReadClient.ReadClientsQuery(id), cancellationToken);
    }

    [HttpPost]
    [Authorize("admin")]
    public async Task<ActionResult<ReadClientsDto>> CreateClient(NewClientRequest newClientRequest, CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST: AddClient");
        var client = await _sender.Send(new AddClientCommand(newClientRequest.Name, newClientRequest.Surname, newClientRequest.Email), cancellationToken);

        return CreatedAtAction("ReadClient", new { id = client.ClientId }, client);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> UpdateClient(UpdateClientRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PUT: ChangeClient");

        try
        {
           var result =  await _sender.Send(new UpdateClientCommand(request.ClientId, request.Name, request.Surname), cancellationToken);
           return Ok(result);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DELETE: DeleteClientWithID");
        await _sender.Send(new DeleteClientCommand(id), cancellationToken);

        return NoContent();
    }
}
