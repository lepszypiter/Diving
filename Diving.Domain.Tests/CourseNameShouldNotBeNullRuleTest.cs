using Diving.Domain.Course;

namespace Diving.Domain.Tests;

public class CourseNameShouldNotBeNullRuleTest
{
    /*[Fact]
    public void CourseNameShouldNotBeNullRule_ShouldBeBroken_WhenNameIsNull()
    {
        // Arrange
        const string name = null;
        var rule = new CourseNameShouldNotBeNullRule(name);

        // Act
        var isBroken = rule.IsBroken();

        // Assert
        isBroken.Should().BeTrue();
    }*/

    [Fact]
    public void CourseNameShouldNotBeNullRule_ShouldBeBroken_WhenNameIsEmpty()
    {
        // Arrange
        const string name = "";
        var rule = new CourseNameShouldNotBeNullRule(name);

        // Act
        var isBroken = rule.IsBroken();

        // Assert
        isBroken.Should().BeTrue();
    }

    [Fact]
    public void CourseNameShouldNotBeNullRuleMessage_ShouldNotBeEmpty_WhenRuleIsBroken()
    {
        // Arrange
        const string name = "";
        var rule = new CourseNameShouldNotBeNullRule(name);

        // Act
        // Assert
        rule.Message.Should().NotBeEmpty();
    }

    [Fact]
    public void CourseNameShouldNotBeNullRule_ShouldNotBeBroken_WhenNameIsNotNullOrEmpty()
    {
        // Arrange
        const string name = "Diving Course";

        var rule = new CourseNameShouldNotBeNullRule(name);

        // Act
        var isBroken = rule.IsBroken();

        // Assert
        isBroken.Should().BeFalse();
    }
}
