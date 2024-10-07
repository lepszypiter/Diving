using Diving.Domain.BuildingBlocks;
using FluentAssertions;

namespace Diving.Domain.Tests;

public class ClientTests
{
    [Fact]
    public void CreateNewClient_ShouldThrowBusinessRuleValidationException_WhenEmailIsInvalid()
    {
        // Arrange
        const string name = "John";
        const string surname = "Doe";
        const string email = "john.doe.com";

        // Act
        var act = () => Client.Client.CreateNewClient(name, surname, email);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage("Email is not valid");
    }

    [Fact]
    public void CreateNewClient_ShouldSetName_WhenNewClientIsCreated()
    {
        // Arrange
        const string name = "John";
        const string surname = "Doe";
        const string email = "john.doe@com";

        // Act
        var client = Client.Client.CreateNewClient(name, surname, email);

        // Assert
        client.Name.Should().Be(name);
        client.Surname.Should().Be(surname);
    }
}
