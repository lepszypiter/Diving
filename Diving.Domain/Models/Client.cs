namespace Diving.Models;

public class ClientData
{
    public long ClientId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? License { get; set; }
    public string? Email { get; set; }
    public List<int>? ClientCourses { get; set; }
}

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

    public long ClientId { get; }
    public string? Name { get;  }
    public string? Surname { get; }
    public string? License { get; }
    public string? Email { get; private set; }
}
