using AutoFixture;
using Diving.Application.ReadCourses;
using Diving.Domain.Course;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.GetCourse;

public class ReadCoursesQueryHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldReturnCourses_WhenStoredInRepository()
    {
        // Arrange
        var courses = new List<Course>
        {
            CreateFakeCourse(),
            CreateFakeCourse()
        };

        var courseRepositoryMock = new Mock<ICourseRepository>();
        courseRepositoryMock.Setup(x => x.ReadAllCourses(It.IsAny<CancellationToken>())).ReturnsAsync(courses);

        var handler = new ReadCoursesQueryHandler(courseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(new ReadCoursesQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(courses, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoCoursesInRepository()
    {
        // Arrange
        var courses = new List<Course>();

        var courseRepositoryMock = new Mock<ICourseRepository>();
        courseRepositoryMock.Setup(x => x.ReadAllCourses(It.IsAny<CancellationToken>())).ReturnsAsync(courses);

        var handler = new ReadCoursesQueryHandler(courseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(new ReadCoursesQuery(), CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    private static Course CreateFakeCourse()
    {
        return new(
            Fixture.Create<long>(),
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<decimal>(),
            new List<Subject>());
    }
}
