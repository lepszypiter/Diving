using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Models;

namespace Diving.Application.ReadClient;

public record ReadClientsQuery(long ClientId) : IQuery<ClientDto>;

internal class ReadClientQueryHandler : IQueryHandler<ReadClientsQuery, ClientDto>
{
    private readonly IClientRepository _clientRepository;

    public ReadClientQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto> Handle(ReadClientsQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetById(request.ClientId, cancellationToken);
        if (client == null)
        {
            throw new("Client not found");
        }

        return new ClientDto(client.ClientId, client.Name, client.Surname, client.Email);
    }
}
