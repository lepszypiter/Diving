using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Models;

namespace Diving.Application.UpdateClient;

public record UpdateClientCommand(long ClientId, string Name, string Surname) : ICommand<ClientDto>;

internal class UpdateClientCommandHandler : ICommandHandler<UpdateClientCommand, ClientDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClientDto> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetById(command.ClientId, cancellationToken);
        if (client is null)
        {
            throw new ArgumentException("Client does not exist");
        }

        client.ModifyClientData(command.Name, command.Surname);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ClientDto(client.ClientId, client.Name, client.Surname, client.Email);
    }
}
