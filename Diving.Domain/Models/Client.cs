namespace Diving.Domain.Models;

public class Client
{
    internal Client(long clientId, string? name, string? surname, string? license, string? email)
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
        if (email.Contains('@'))
        {
            return new Client(0, name, surname, null, email);
        }

        throw new ArgumentException("Email is not valid");
    }

    public long ClientId { get; }
    public string? Name { get; private set; }
    public string? Surname { get; private set;  }
    public string? License { get; }
    public string? Email { get; private set; }

    public void ModifyClientData(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}
