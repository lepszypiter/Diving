using Diving.Domain.BuildingBlocks;

namespace Diving.Domain.Clients;

public class ClientHaveValidEmailRule : IBusinessRule
{
    private readonly string _email;

    internal ClientHaveValidEmailRule(string email)
    {
        _email = email;
    }

    public bool IsBroken() => string.IsNullOrWhiteSpace(_email) || !_email.Contains('@');

    public string Message => "Email is not valid";
}
