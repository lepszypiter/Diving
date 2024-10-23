namespace Diving.Application.ReadClients;

public record ReadClientsQuery : ICommand<IReadOnlyCollection<ReadClientsDto>>;
internal class ReadClientsQueryHandler : UnitOfWorkCommandHandler<ReadClientsQuery , IReadOnlyCollection<ReadClientsDto>>
{
    private readonly IClientRepository _clientRepository;

    public ReadClientsQueryHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _clientRepository = clientRepository;
    }

    protected override async Task<IReadOnlyCollection<ReadClientsDto>> HandleCommand(ReadClientsQuery command, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.ReadAllClients(cancellationToken);

        return clients
            .Select(x => new ReadClientsDto(x.ClientId, x.Name, x.Surname, x.Email))
            .ToList();
    }
}
