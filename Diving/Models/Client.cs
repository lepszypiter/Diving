namespace Diving.Models;

public class Client
{
    public long ClientId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? License { get; set; }
    public string? Email { get; set; }
    public ICollection<int>? ClientCourses { get; set; }
}