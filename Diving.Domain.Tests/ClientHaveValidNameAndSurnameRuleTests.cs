using Diving.Domain.Clients;
using FluentAssertions;

namespace Diving.Domain.Tests;

public class ClientHaveValidNameAndSurnameRuleTests
{
    [Fact]
    public void ClientHaveValidNameAndSurnameRule_ShouldBeBroken_WhenNameIsInvalid()
    {
        // Arrange
        const string name = "J";
        const string surname = "Doe";
        var rule = new ClientHaveValidNameAndSurnameRule(name, surname);

        // Act
        var isBroken = rule.IsBroken();

        // Assert
        isBroken.Should().BeTrue();
    }

    [Fact]
    public void ClientHaveValidNameAndSurnameRuleMessage_ShouldNotBeEmpty_WhenRuleIsBroken()
    {
        // Arrange
        const string name = "J";
        const string surname = "Doe";
        var rule = new ClientHaveValidNameAndSurnameRule(name, surname);

        // Act
        // Assert
        rule.Message.Should().NotBeEmpty();
    }

    [Fact]
    public void ClientHaveValidNameAndSurnameRule_ShouldNotBeBroken_WhenNameAndSurnameAreValid()
    {
        // Arrange
        const string name = "John";
        const string surname = "Doe";

        var rule = new ClientHaveValidNameAndSurnameRule(name, surname);

        // Act
        var isBroken = rule.IsBroken();

        // Assert
        isBroken.Should().BeFalse();
    }
}
