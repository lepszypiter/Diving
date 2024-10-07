using AutoFixture;
using Diving.Application.GetCourse;
using Diving.Domain.Course;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.GetCourse;

public class GetCoursesQueryHandlerTests
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
        courseRepositoryMock.Setup(x => x.GetAllCourses()).ReturnsAsync(courses);

        var handler = new GetCoursesQueryHandler(courseRepositoryMock.Object);

        // Act
        var result = await handler.Handle();

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
        courseRepositoryMock.Setup(x => x.GetAllCourses()).ReturnsAsync(courses);

        var handler = new GetCoursesQueryHandler(courseRepositoryMock.Object);

        // Act
        var result = await handler.Handle();

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
            null);
    }
}
