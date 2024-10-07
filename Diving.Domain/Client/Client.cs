using Diving.Domain.BuildingBlocks;

namespace Diving.Domain.Client;

public class Client : Entity
{
    internal Client(long clientId, string name, string surname, string? license, string email)
    {
        ClientId = clientId;
        Name = name;
        Surname = surname;
        License = license;
        Email = email;
    }

    private Client()
    {
    }

    public static Client CreateNewClient(string name, string surname, string email)
    {
        CheckRule(new ClientHaveValidEmailRule(email));
        CheckRule(new ClientHaveValidNameAndSurnameRule(name, surname));

        return new Client(0, name, surname, null, email);
    }

    public long ClientId { get; }
    public string Name { get; private set; } = null!;
    public string Surname { get; private set;  } = null!;
    public string? License { get; }
    public string Email { get; private set; } = null!;

    public void ModifyClientData(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}
