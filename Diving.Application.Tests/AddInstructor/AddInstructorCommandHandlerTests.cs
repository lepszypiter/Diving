using AutoFixture;
using Diving.Application.AddInstructor;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.AddInstructor;

public class AddInstructorCommandHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldReturnInstructor_WhenInstructorAdded()
    {
        // Arrange
        var newInstructorDto = CreateFakeNewInstructorDto();

        var instructorRepositoryMock = new Mock<IInstructorRepository>();

        var handler = new AddInstructorCommandHandler(instructorRepositoryMock.Object);

        // Act
        var result = await handler.Handle(newInstructorDto);

        // Assert
        result.Should().BeEquivalentTo(newInstructorDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveDataInRepository_WhenNewInstructorIsAdded()
    {
        // Arrange
        var newInstructorDto = CreateFakeNewInstructorDto();

        var instructorRepositoryMock = new Mock<IInstructorRepository>();

        var handler = new AddInstructorCommandHandler(instructorRepositoryMock.Object);

        // Act
        await handler.Handle(newInstructorDto);

        // Assert
        instructorRepositoryMock.Verify(x => x.Add(It.IsAny<Instructor>()), Times.Once);
        instructorRepositoryMock.Verify(x => x.Save(), Times.Once);
    }

    private static NewInstructorDto CreateFakeNewInstructorDto()
    {
        return new(
            Fixture.Create<string>(),
            Fixture.Create<string>());
    }
}
