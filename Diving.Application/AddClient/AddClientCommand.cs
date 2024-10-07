using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Clients;

namespace Diving.Application.AddClient;

public record AddClientCommand(string Name, string Surname, string Email) : ICommand<Client>;
