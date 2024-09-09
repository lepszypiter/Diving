using Diving.Domain.Models;
using FluentAssertions;

namespace Diving.Domain.Tests;

public class ClientTests
{
    [Fact]
    public void CreateNewClient_ShouldThrowException_WhenEmailIsInvalid()
    {
        // Arrange
        const string name = "John";
        const string surname = "Doe";
        const string email = "john.doe.com";

        // Act
        var act = () => Client.CreateNewClient(name, surname, email);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Email is not valid");
    }

    [Fact]
    public void CreateNewClient_ShouldNotThrowException_WhenEmailIsValid()
    {
        // Arrange
        const string name = "John";
        const string surname = "Doe";
        const string email = "john.doe@com";

        // Act
        var act = () => Client.CreateNewClient(name, surname, email);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void CreateNewClient_ShouldSetName_WhenNewClientIsCreated()
    {
        // Arrange
        const string name = "John";
        const string surname = "Doe";
        const string email = "john.doe@com";

        // Act
        var client = Client.CreateNewClient(name, surname, email);

        // Assert
        client.Name.Should().Be(name);
        client.Surname.Should().Be(surname);
    }
}
