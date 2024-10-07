using AutoFixture;
using Diving.Application.ModifyInstructor;
using Diving.Domain.Instructor;
using Diving.Domain.Models;
using FluentAssertions;
using Moq;

namespace Diving.Application.Tests.ModifyInstuctor;

public class ModifyInstructorsCommandHandlerTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task ShouldModifyInstructor_WhenInstructorChanged()
    {
        // Arrange
        var modifyInstructorDto = CreateFakeNewInstructorDto();

        var instructorRepositoryMock = new Mock<IInstructorRepository>();
        instructorRepositoryMock.Setup(x => x.GetById(modifyInstructorDto.InstructorId)).ReturnsAsync(CreateFakeInstructor(modifyInstructorDto.InstructorId));
        var handler = new ModifyInstructorsCommandHandler(instructorRepositoryMock.Object);

        // Act
        var result = await handler.Handle(modifyInstructorDto);

        // Assert
        result.Should().BeEquivalentTo(modifyInstructorDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveModifiedInstructor_WhenInstructorExists()
    {
        // Arrange
        var modifyInstructorDto = CreateFakeNewInstructorDto();

        var instructorRepositoryMock = new Mock<IInstructorRepository>();
        instructorRepositoryMock.Setup(x => x.GetById(modifyInstructorDto.InstructorId)).ReturnsAsync(CreateFakeInstructor(modifyInstructorDto.InstructorId));
        var handler = new ModifyInstructorsCommandHandler(instructorRepositoryMock.Object);

        // Act
        await handler.Handle(modifyInstructorDto);

        // Assert
        instructorRepositoryMock.Verify(x => x.Save(), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowArgumentException_WhenInstructorDoesNotExist()
    {
        // Arrange
        var modifyInstructorDto = CreateFakeNewInstructorDto();

        var instructorRepositoryMock = new Mock<IInstructorRepository>();

        var handler = new ModifyInstructorsCommandHandler(instructorRepositoryMock.Object);

        // Act
        Func<Task> act = async () => await handler.Handle(modifyInstructorDto);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    private static ModifyInstructorDto CreateFakeNewInstructorDto()
    {
        return new(
            Fixture.Create<long>(),
            Fixture.Create<string>(),
            Fixture.Create<string>());
    }

    private static Instructor CreateFakeInstructor(long id)
    {
        return new(
            id,
            Fixture.Create<string>(),
            Fixture.Create<string>());
    }
}
