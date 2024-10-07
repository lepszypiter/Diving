using Diving.Domain.Clients;
using Diving.Domain.Models;

namespace Diving.Application.ModifyClients;

public class ModifyClientsCommandHandler
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ModifyClientsCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Client> Handle(ModifyClientDto modifyClientDto, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetById(modifyClientDto.ClientId);
        if (client is null)
        {
            throw new ArgumentException("Client does not exist");
        }

        client.ModifyClientData(modifyClientDto.Name, modifyClientDto.Surname);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return client;
    }
}
