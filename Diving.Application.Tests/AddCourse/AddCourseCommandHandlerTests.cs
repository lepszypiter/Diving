using AutoFixture;
using Diving.Application.AddCourse;
using Diving.Domain.Course;
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
        var newCourseDto = CreateFakeAddCourseCommand();

        var courseRepositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var handler = new AddCourseCommandHandler(courseRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(newCourseDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(newCourseDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveDataInRepository_WhenNewCourseIsAdded()
    {
        // Arrange
        var newCourseDto = CreateFakeAddCourseCommand();

        var courseRepositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var handler = new AddCourseCommandHandler(courseRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        await handler.Handle(newCourseDto, CancellationToken.None);

        // Assert
        courseRepositoryMock.Verify(x => x.Add(It.IsAny<Course>()), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    private static AddCourseCommand CreateFakeAddCourseCommand()
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
