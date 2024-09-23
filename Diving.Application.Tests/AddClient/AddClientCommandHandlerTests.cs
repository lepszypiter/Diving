using AutoFixture;
using Diving.Application.AddClient;
using Diving.Domain.Clients;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.AddClient;

public class AddClientCommandHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldReturnClient_WhenClientAdded()
    {
        // Arrange
        var newClientDto = CreateFakeAddClientCommand();

        var clientRepositoryMock = new Mock<IClientRepository>();

        var handler = new AddClientCommandHandler(clientRepositoryMock.Object);

        // Act
        var result = await handler.Handle(newClientDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(newClientDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveDataInRepository_WhenNewClientIsAdded()
    {
        // Arrange
        var newClientDto = CreateFakeAddClientCommand();

        var clientRepositoryMock = new Mock<IClientRepository>();

        var handler = new AddClientCommandHandler(clientRepositoryMock.Object);

        // Act
        await handler.Handle(newClientDto, CancellationToken.None);

        // Assert
        clientRepositoryMock.Verify(x => x.Add(It.IsAny<Client>()), Times.Once);
        clientRepositoryMock.Verify(x => x.Save(), Times.Once);
    }

    private static AddClientCommand CreateFakeAddClientCommand()
    {
        return new(
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            "aa@aa.pl");
    }
}
