using Diving.Domain.BuildingBlocks;

namespace Diving.Domain.Clients;

public class ClientHaveValidNameAndSurnameRule : IBusinessRule
{
    private readonly string _name, _surname;

    internal ClientHaveValidNameAndSurnameRule(string name, string surname)
    {
        _name = name;
        _surname = surname;
    }

    public bool IsBroken() => _name.Length <= 2 || _surname.Length <= 2;

    public string Message => "Name or surname is not valid";
}
