using AutoFixture;
using Diving.Application.GetClients;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.GetClient;

public class GetClientsQueryHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldReturnClients_WhenStoredInRepository()
    {
        // Arrange
        var clients = new List<Client>
        {
            CreateFakeClient(),
            CreateFakeClient()
        };

        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(x => x.GetAllClients()).ReturnsAsync(clients);

        var handler = new GetClientsQueryHandler(clientRepositoryMock.Object);

        // Act
        var result = await handler.Handle();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(clients, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoClientsInRepository()
    {
        // Arrange
        var clients = new List<Client>();

        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(x => x.GetAllClients()).ReturnsAsync(clients);

        var handler = new GetClientsQueryHandler(clientRepositoryMock.Object);

        // Act
        var result = await handler.Handle();

        // Assert
        result.Should().BeEmpty();
    }

    private static Client CreateFakeClient()
    {
        return new(
            Fixture.Create<long>(),
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            "aa@aa.pl");
    }
}
