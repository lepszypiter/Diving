using Diving.Domain.Client;

namespace Diving.Application.AddClient;

internal class AddClientCommandHandler : ICommandHandler<AddClientCommand, Client>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Client> Handle(AddClientCommand command, CancellationToken cancellationToken)
    {
        var client = Client.CreateNewClient(command.Name, command.Surname, command.Email);

            await _clientRepository.Add(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return client;
    }
}
