namespace Diving.Application.UpdateClient;

public record UpdateClientCommand(long ClientId, string Name, string Surname) : ICommand<ClientDto>;

internal class UpdateClientCommandHandler : UnitOfWorkCommandHandler<UpdateClientCommand, ClientDto>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _clientRepository = clientRepository;
    }

    protected override async Task<ClientDto> HandleCommand(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetById(command.ClientId, cancellationToken);
        if (client is null)
        {
            throw new ArgumentException("Client does not exist");
        }

        client.ModifyClientData(command.Name, command.Surname);
        await _clientRepository.Add(client);
        return new ClientDto(client.ClientId, client.Name, client.Surname, client.Email);
    }
}
