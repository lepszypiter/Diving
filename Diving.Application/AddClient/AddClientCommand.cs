using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Client;

namespace Diving.Application.AddClient;

public record AddClientCommand(string Name, string Surname, string Email) : ICommand<Client>;
