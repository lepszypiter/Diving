using Diving.Domain.BuildingBlocks;

namespace Diving.Domain;

public class Subject : Entity
{
    internal Subject(long subjectId, string name)
    {
        SubjectId = subjectId;
        Name = name;
    }

    public long SubjectId { get; }
    public string Name { get; set; }

    public static Subject CreateNewSubject(string name)
    {
        return new(0, name);
    }

    public static Subject CreateNewSubject(int commandSubjectId, string commandSubjectName)
    {
        throw new NotImplementedException();
    }

    public static Subject CreateNewSubject(Subject commandXxx)
    {
        throw new NotImplementedException();
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
