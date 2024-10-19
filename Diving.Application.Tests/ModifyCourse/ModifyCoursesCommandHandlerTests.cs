using Diving.Application.UpdateCourse;
using Diving.Domain.Course;
using Diving.Domain.Models;

namespace Diving.Application.Tests.ModifyCourse;

public class ModifyCoursesCommandHandlerTests
{
    private static readonly Fixture Fixture = new();
    private readonly Mock<ICourseRepository> _courseRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateCourseCommandHandler _handler;

    public ModifyCoursesCommandHandlerTests()
    {
        _courseRepositoryMock = new Mock<ICourseRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateCourseCommandHandler(_courseRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldReturnModifiedCourse_WhenCourseChanged()
    {
        // Arrange
        var modifyCourseDto = CreateUpdateCourseCommand();

        _courseRepositoryMock.Setup(x => x.GetById(
            modifyCourseDto.CourseId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateFakeCourse(modifyCourseDto.CourseId));

        // Act
        var result = await _handler.Handle(modifyCourseDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(modifyCourseDto, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ShouldSaveModifiedCourse_WhenCourseChanged()
    {
        // Arrange
        var modifyCourseDto = CreateUpdateCourseCommand();

        _courseRepositoryMock.Setup(x => x.GetById(
            modifyCourseDto.CourseId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateFakeCourse(modifyCourseDto.CourseId));

        // Act
        await _handler.Handle(modifyCourseDto, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowArgumentException_WhenCourseDoesNotExist()
    {
        // Arrange
        var modifyCourseDto = CreateUpdateCourseCommand();

        // Act
        var act = async () => await _handler.Handle(modifyCourseDto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    private static UpdateCourseCommand CreateUpdateCourseCommand()
    {
        return new(
            Fixture.Create<long>(),
            Fixture.Create<string>()
        );
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
            Fixture.Create<decimal>(),
            new List<Subject>()
        );
    }
}
