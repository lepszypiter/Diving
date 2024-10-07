using Diving.Application.GetClients;
using Diving.Domain.Models;
using Diving.Application.Abstarction.Messaging;

namespace Diving.Application.ReadClients;

public record ReadClientsQuery() : IQuery<IReadOnlyCollection<ClientDto>>;

internal class ReadClientsQueryHandler : IQueryHandler<ReadClientsQuery , IReadOnlyCollection<ClientDto>>
{
    private readonly IClientRepository _clientRepository;

    public ReadClientsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IReadOnlyCollection<ClientDto>> Handle(ReadClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.ReadAllClients(cancellationToken);

        return clients
            .Select(x => new ClientDto(x.ClientId, x.Name, x.Surname, x.Email))
            .ToList();
    }
}
