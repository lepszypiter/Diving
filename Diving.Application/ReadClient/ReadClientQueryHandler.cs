namespace Diving.Application.ReadClient;

public record ReadClientQuery(long ClientId) : IQuery<ReadClientDto>;

internal class ReadClientQueryHandler : IQueryHandler<ReadClientQuery, ReadClientDto>
{
    private readonly IClientRepository _clientRepository;

    public ReadClientQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ReadClientDto> Handle(ReadClientQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetById(request.ClientId, cancellationToken);
        if (client == null)
        {
            throw new("Client not found");
        }

        return new ReadClientDto(client.ClientId, client.Name, client.Surname, client.Email);
    }
}
