using AutoFixture;
using Diving.Application.GetInstructor;
using Diving.Domain.Instructor;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.GetInstructor;

public class GetInstructorsQueryHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldReturnInstructors_WhenStoredInRepository()
    {
        // Arrange
        var instructors = new List<Instructor>
        {
            CreateFakeInstructor(),
            CreateFakeInstructor()
        };

        var instructorRepositoryMock = new Mock<IInstructorRepository>();
        instructorRepositoryMock.Setup(x => x.GetAllInstructors()).ReturnsAsync(instructors);

        var handler = new GetInstructorsQueryHandler(instructorRepositoryMock.Object);

        // Act
        var result = await handler.Handle();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(instructors, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoInstructorsInRepository()
    {
        // Arrange
        var instructors = new List<Instructor>();

        var instructorRepositoryMock = new Mock<IInstructorRepository>();
        instructorRepositoryMock.Setup(x => x.GetAllInstructors()).ReturnsAsync(instructors);

        var handler = new GetInstructorsQueryHandler(instructorRepositoryMock.Object);

        // Act
        var result = await handler.Handle();

        // Assert
        result.Should().BeEmpty();
    }

    private static Instructor CreateFakeInstructor()
    {
        return new(
            Fixture.Create<long>(),
            Fixture.Create<string>(),
            Fixture.Create<string>()
        );
    }
}
