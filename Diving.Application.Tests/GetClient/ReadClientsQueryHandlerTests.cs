using Diving.Application.ReadClients;
using Diving.Domain.Client;
using Diving.Domain.Models;

namespace Diving.Application.Tests.GetClient;

public class ReadClientsQueryHandlerTests
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
        clientRepositoryMock.Setup(x => x.ReadAllClients(It.IsAny<CancellationToken>())).ReturnsAsync(clients);

        var handler = new ReadClientsQueryHandler(clientRepositoryMock.Object, new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(new ReadClientsQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(clients, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoClientsInRepository()
    {
        // Arrange
        var clients = ArraySegment<Client>.Empty;

        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(x => x.ReadAllClients(It.IsAny<CancellationToken>())).ReturnsAsync(clients);

        var handler = new ReadClientsQueryHandler(clientRepositoryMock.Object, new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(new ReadClientsQuery(), CancellationToken.None);

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
