using Diving.Domain.Client;

namespace Diving.Application.AddClient;

internal class AddClientCommandHandler : UnitOfWorkCommandHandler<AddClientCommand, Client>
{
    private readonly IClientRepository _clientRepository;

    public AddClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _clientRepository = clientRepository;
    }

    protected override async Task<Client> HandleCommand(AddClientCommand command, CancellationToken cancellationToken)
    {
        var client = Client.CreateNewClient(command.Name, command.Surname, command.Email);

        await _clientRepository.Add(client);
        return client;
    }
}
