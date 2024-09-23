using Diving.Domain.BuildingBlocks;

namespace Diving.Domain.Models;

public class Instructor : Entity
{
    internal Instructor(long instructorId, string name, string surname)
    {
        InstructorId = instructorId;
        Name = name;
        Surname = surname;
    }

    private Instructor()
    {
    }

    public static Instructor CreateNewInstructor(string name, string surname)
    {
        return new(0, name, surname);
    }

    public long InstructorId { get;}
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;

    public void ModifyInstructorData(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}
