namespace Diving.Domain.Models;

public class Client
{
    public static Client CreateNewClient(string name, string surname, string email)
    {
        return new(0, name, surname, null, email);
    }

    private Client()
    {
    }

    internal Client(long clientId, string? name, string? surname, string? license, string? email)
    {
        ClientId = clientId;
        Name = name;
        Surname = surname;
        License = license;
        Email = email;
    }

    public long ClientId { get; }
    public string? Name { get;  }
    public string? Surname { get; }
    public string? License { get; }
    public string? Email { get; private set; }
}
