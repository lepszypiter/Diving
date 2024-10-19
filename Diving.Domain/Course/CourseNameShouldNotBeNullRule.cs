using Diving.Domain.BuildingBlocks;

namespace Diving.Domain.Course;

public class CourseNameShouldNotBeNullRule : IBusinessRule
{
    private readonly string _name;

    internal CourseNameShouldNotBeNullRule(string name)
    {
        _name = name;
    }

    public bool IsBroken() => string.IsNullOrWhiteSpace(_name);

    public string Message => "Course name should not be null or empty";
}
