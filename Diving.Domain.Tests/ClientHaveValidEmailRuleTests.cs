using Diving.Domain.Client;

namespace Diving.Domain.Tests;

public class ClientHaveValidEmailRuleTests
{
    [Fact]
    public void ClientHaveValidEmailRule_ShouldBeBroken_WhenEmailIsInvalid()
    {
        // Arrange
        const string email = "john.doe.com";
        var rule = new ClientHaveValidEmailRule(email);

        // Act
        var isBroken = rule.IsBroken();

        // Assert
        isBroken.Should().BeTrue();
    }

    [Fact]
    public void ClientHaveValidEmailRuleMessage_ShouldNotBeEmpty_WhenRuleIsBroken()
    {
        // Arrange
        const string email = "john.doe.com";
        var rule = new ClientHaveValidEmailRule(email);

        // Act
       // Assert
        rule.Message.Should().NotBeEmpty();
    }

    [Fact]
    public void ClientHaveValidEmailRule_ShouldNotBeBroken_WhenEmailIsValid()
    {
        // Arrange
        const string email = "john.doe@com";

        var rule = new ClientHaveValidEmailRule(email);

        // Act
        var isBroken = rule.IsBroken();

        // Assert
        isBroken.Should().BeFalse();
    }
}
