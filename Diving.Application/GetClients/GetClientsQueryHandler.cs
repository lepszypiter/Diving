using Diving.Domain.Models;

namespace Diving.Application.GetClients;

public class GetClientsQueryHandler
{
    private readonly IClientRepository _clientRepository;

    public GetClientsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IReadOnlyCollection<ClientDto>> Handle()
    {
        var clients = await _clientRepository.GetAllClients();

        return clients
            .Select(x => new ClientDto(x.ClientId, x.Name, x.Surname, x.Email))
            .ToList();
    }
}
