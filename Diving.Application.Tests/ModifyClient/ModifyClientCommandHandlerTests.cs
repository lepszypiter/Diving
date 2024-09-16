using AutoFixture;
using Diving.Application.ModifyClients;
using Diving.Domain.Clients;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.ModifyClient;

public class ModifyClientsCommandHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldReturnModifiedClient_WhenClientChanged()
    {
        // Arrange
        var modifyClientDto = CreateFakeNewClientDto();

        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(x => x.GetById(modifyClientDto.ClientId)).ReturnsAsync(CreateFakeClient(modifyClientDto.ClientId));
        var handler = new ModifyClientsCommandHandler(clientRepositoryMock.Object);

        // Act
        var result = await handler.Handle(modifyClientDto);

        // Assert
        result.Should().BeEquivalentTo(modifyClientDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveModifiedClient_WhenClientChanged()
    {
        // Arrange
        var modifyClientDto = CreateFakeNewClientDto();

        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(x => x.GetById(modifyClientDto.ClientId)).ReturnsAsync(CreateFakeClient(modifyClientDto.ClientId));
        var handler = new ModifyClientsCommandHandler(clientRepositoryMock.Object);

        // Act
        await handler.Handle(modifyClientDto);

        // Assert
        clientRepositoryMock.Verify(x => x.Save(), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowArgumentException_WhenClientDoesNotExist()
    {
        // Arrange
        var modifyClientDto = CreateFakeNewClientDto();

        var clientRepositoryMock = new Mock<IClientRepository>();

        var handler = new ModifyClientsCommandHandler(clientRepositoryMock.Object);

        // Act
        Func<Task> act = async () => await handler.Handle(modifyClientDto);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    private static ModifyClientDto CreateFakeNewClientDto()
    {
        return new(
            Fixture.Create<long>(),
            Fixture.Create<string>(),
            Fixture.Create<string>());
    }

    private static Client CreateFakeClient(long clientId)
    {
        return new(
            clientId,
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            "aa@aa.pl");
    }
}
