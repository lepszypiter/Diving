namespace Diving.Application.ReadClient;

public record ReadClientQuery(long ClientId) : ICommand<ReadClientDto>;
internal class ReadClientQueryHandler : UnitOfWorkCommandHandler<ReadClientQuery, ReadClientDto>
{
    private readonly IClientRepository _clientRepository;

    public ReadClientQueryHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _clientRepository = clientRepository;
    }

    protected override async Task<ReadClientDto> HandleCommand(ReadClientQuery command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetById(command.ClientId, cancellationToken);
        if (client == null)
        {
            throw new("Client not found");
        }

        return new ReadClientDto(client.ClientId, client.Name, client.Surname, client.Email);
    }
}
