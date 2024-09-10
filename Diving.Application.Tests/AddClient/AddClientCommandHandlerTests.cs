using AutoFixture;
using Diving.Application.AddClient;
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
        var newClientDto = CreateFakeNewClientDto();

        var clientRepositoryMock = new Mock<IClientRepository>();

        var handler = new AddClientCommandHandler(clientRepositoryMock.Object);

        // Act
        var result = await handler.Handle(newClientDto);

        // Assert
        result.Should().BeEquivalentTo(newClientDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveDataInRepository_WhenNewClientIsAdded()
    {
        // Arrange
        var newClientDto = CreateFakeNewClientDto();

        var clientRepositoryMock = new Mock<IClientRepository>();

        var handler = new AddClientCommandHandler(clientRepositoryMock.Object);

        // Act
        await handler.Handle(newClientDto);

        // Assert
        clientRepositoryMock.Verify(x => x.Add(It.IsAny<Client>()), Times.Once);
        clientRepositoryMock.Verify(x => x.Save(), Times.Once);
    }

    private static NewClientDto CreateFakeNewClientDto()
    {
        return new(
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            "aa@aa.pl");
    }
}
