using Diving.Domain.BuildingBlocks;

namespace Diving.Domain.Course;

public class Subject : Entity
{
    internal Subject(long subjectId, string name)
    {
        SubjectId = subjectId;
        Name = name;
    }

    public long SubjectId { get; }
    public string Name { get; set; }
    public long CourseId { get; set; }

    public static Subject CreateNewSubject(string name)
    {
        return new(0, name);
    }

    public void ModifySubjectData(string name)
    {
        Name = name;
    }

    private Subject()
    {
        Name = string.Empty;
    }
}
