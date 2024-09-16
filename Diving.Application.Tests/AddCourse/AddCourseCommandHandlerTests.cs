using AutoFixture;
using Diving.Application.AddCourse;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.AddCourse;

public class AddCourseCommandHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldReturnCourse_WhenCourseAdded()
    {
        // Arrange
        var newCourseDto = CreateFakeNewCourseDto();

        var courseRepositoryMock = new Mock<ICourseRepository>();

        var handler = new AddCourseCommandHandler(courseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(newCourseDto);

        // Assert
        result.Should().BeEquivalentTo(newCourseDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveDataInRepository_WhenNewCourseIsAdded()
    {
        // Arrange
        var newCourseDto = CreateFakeNewCourseDto();

        var courseRepositoryMock = new Mock<ICourseRepository>();

        var handler = new AddCourseCommandHandler(courseRepositoryMock.Object);

        // Act
        await handler.Handle(newCourseDto);

        // Assert
        courseRepositoryMock.Verify(x => x.Add(It.IsAny<Course>()), Times.Once);
        courseRepositoryMock.Verify(x => x.Save(), Times.Once);
    }

    private static NewCourseDto CreateFakeNewCourseDto()
    {
        return new(
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<decimal>());
    }
}
