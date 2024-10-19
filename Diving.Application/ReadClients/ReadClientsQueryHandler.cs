namespace Diving.Application.ReadClients;

public record ReadClientsQuery : IQuery<IReadOnlyCollection<ReadClientsDto>>;

internal class ReadClientsQueryHandler : IQueryHandler<ReadClientsQuery , IReadOnlyCollection<ReadClientsDto>>
{
    private readonly IClientRepository _clientRepository;

    public ReadClientsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IReadOnlyCollection<ReadClientsDto>> Handle(ReadClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.ReadAllClients(cancellationToken);

        return clients
            .Select(x => new ReadClientsDto(x.ClientId, x.Name, x.Surname, x.Email))
            .ToList();
    }
}
