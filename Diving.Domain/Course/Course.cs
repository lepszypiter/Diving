using Diving.Domain.BuildingBlocks;

namespace Diving.Domain.Models;

public class Course : Entity
{
    internal Course(long courseId, string name, string instructor, int? hoursOnOpenWater, int? hoursOnPool, int? hoursOfLectures, decimal? price)
    {
        CourseId = courseId;
        Name = name;
        Instructor = instructor;
        HoursOnOpenWater = hoursOnOpenWater;
        HoursOnPool = hoursOnPool;
        HoursOfLectures = hoursOfLectures;
        Price = price;
    }

    private Course()
    {
    }

    public static Course CreateNewCourse(string name, string instructor, int? hoursOnOpenWater, int? hoursOnPool, int? hoursOfLectures, decimal? price)
    {
        return new(0, name, instructor, hoursOnOpenWater, hoursOnPool, hoursOfLectures, price);
    }

    public long CourseId { get;}
    public string Name { get; private set; } = null!;
    public string Instructor { get; private set; } = null!;
    public int? HoursOnOpenWater { get; private set; }
    public int? HoursOnPool { get; private set; }
    public int? HoursOfLectures { get; private set; }
    public decimal? Price { get; private set; }

    public void ModifyCourseData(string name, string instructor, int? hoursOnOpenWater, int? hoursOnPool, int? hoursOfLectures, decimal? price)
    {
        Name = name;
        Instructor = instructor;
        HoursOnOpenWater = hoursOnOpenWater;
        HoursOnPool = hoursOnPool;
        HoursOfLectures = hoursOfLectures;
        Price = price;
    }
}
