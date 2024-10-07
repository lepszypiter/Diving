using AutoFixture;
using Diving.Application.ModifyClients;
using Diving.Domain.Client;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.ModifyClient;

public class ModifyClientsCommandHandlerTests
{
    private static readonly Fixture Fixture = new();
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ModifyClientsCommandHandler _handler;

    public ModifyClientsCommandHandlerTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new ModifyClientsCommandHandler(_clientRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldReturnModifiedClient_WhenClientChanged()
    {
        // Arrange
        var modifyClientDto = CreateFakeNewClientDto();

        _clientRepositoryMock.Setup(x => x.GetById(
            modifyClientDto.ClientId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateFakeClient(modifyClientDto.ClientId));

        // Act
        var result = await _handler.Handle(modifyClientDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(modifyClientDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveModifiedClient_WhenClientChanged()
    {
        // Arrange
        var modifyClientDto = CreateFakeNewClientDto();

        _clientRepositoryMock.Setup(x => x.GetById(
            modifyClientDto.ClientId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateFakeClient(modifyClientDto.ClientId));

        // Act
        await _handler.Handle(modifyClientDto, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowArgumentException_WhenClientDoesNotExist()
    {
        // Arrange
        var modifyClientDto = CreateFakeNewClientDto();

        // Act
        var act = async () => await _handler.Handle(modifyClientDto, CancellationToken.None);

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
