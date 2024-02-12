namespace Diving.Models;

public class Course
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Instructor { get; set; }
    public int? HoursOnOpenWater { get; set; }
    public int? HoursOnPool { get; set; }
    public int? HoursOfLectures { get; set; }
    public decimal? Price { get; set; }
}