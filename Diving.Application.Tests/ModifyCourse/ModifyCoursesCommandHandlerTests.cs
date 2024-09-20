using AutoFixture;
using Diving.Application.ModifyCourse;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.ModifyCourse;

public class ModifyCoursesCommandHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldModifyCourse_WhenCourseChanged()
    {
        // Arrange
        var modifyCourseDto = CreateFakeNewCourseDto();

        var courseRepositoryMock = new Mock<ICourseRepository>();
        courseRepositoryMock.Setup(x => x.GetById(modifyCourseDto.CourseId)).ReturnsAsync(CreateFakeCourse(modifyCourseDto.CourseId));
        var handler = new ModifyCoursesCommandHandler(courseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(modifyCourseDto);

        // Assert
        result.Should().BeEquivalentTo(modifyCourseDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveModifiedCourse_WhenCourseExists()
    {
        // Arrange
        var modifyCourseDto = CreateFakeNewCourseDto();

        var courseRepositoryMock = new Mock<ICourseRepository>();
        courseRepositoryMock.Setup(x => x.GetById(modifyCourseDto.CourseId)).ReturnsAsync(CreateFakeCourse(modifyCourseDto.CourseId));
        var handler = new ModifyCoursesCommandHandler(courseRepositoryMock.Object);

        // Act
        await handler.Handle(modifyCourseDto);

        // Assert
        courseRepositoryMock.Verify(x => x.Save(), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowArgumentException_WhenCourseDoesNotExist()
    {
        // Arrange
        var modifyCourseDto = CreateFakeNewCourseDto();

        var courseRepositoryMock = new Mock<ICourseRepository>();

        var handler = new ModifyCoursesCommandHandler(courseRepositoryMock.Object);

        // Act
        Func<Task> act = async () => await handler.Handle(modifyCourseDto);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    private static ModifyCourseDto CreateFakeNewCourseDto()
    {
        return new(
            Fixture.Create<long>(),
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<decimal>());
    }

    private static Course CreateFakeCourse(long courseId)
    {
        return new(
            courseId,
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<decimal>());
    }
}
