namespace Diving.APi.IntegrationTest;

public class SubjectApiTest
{
    private readonly HttpClient _client = new();
    private static readonly Fixture Fixture = new();
    private static long _testCourseId;

    public record ReadSubjectsDto(long SubjectId, string Name);
    public record AddSubjectCommand(long CourseId, string Name);
    public record AddCourseCommand(string Name, string Instructor, int HoursOnOpenWater, int HoursOnPool, int HoursOfLectures, decimal Price);

    public record ReadCourseCommand(
        long CourseId,
        string Name,
        string Instructor,
        int HoursOnOpenWater,
        int HoursOnPool,
        int HoursOfLectures,
        decimal Price);

    public SubjectApiTest()
    {
        var authenticationClient = new AuthenticationClient();
        _client.BaseAddress = new Uri("http://localhost:5175");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        var token = authenticationClient.GetToken().Result;
        _client.DefaultRequestHeaders.Add("Authorization", token);
    }

    [Fact]
    public async Task CreateCourseAndSubject()
    {
        var newCourseDto = CreateFakeAddCourseCommand();
        var course = await PostCourseAsync(newCourseDto);
        course.Should().NotBeNull();
        course.Should().BeEquivalentTo(newCourseDto, x => x.ExcludingMissingMembers());
        _testCourseId = course!.CourseId;
        var newSubjectDto = new AddSubjectCommand(_testCourseId, Fixture.Create<string>());
        var subject = await PostSubjectAsync(_testCourseId, newSubjectDto);
        subject.Should().NotBeNull();
        subject.Should().BeEquivalentTo(newSubjectDto, x => x.ExcludingMissingMembers());
    }

    private async Task<ReadCourseCommand> PostSubjectAsync(long courseId, AddSubjectCommand newSubjectDto)
    {
        var response = await _client.PostAsJsonAsync(
            $"/api/course/{courseId}/subjects", newSubjectDto);
        return response.Content.ReadAsAsync<ReadCourseCommand>().Result;
    }

    private static AddCourseCommand CreateFakeAddCourseCommand()
    {
        return new(
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<int>(),
            Fixture.Create<decimal>());
    }

    private async Task<ReadCourseCommand?> PostCourseAsync(AddCourseCommand fakeCourse)
    {
        var response = await _client.PostAsJsonAsync(
            "/api/Course", fakeCourse);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return response.Content.ReadAsAsync<ReadCourseCommand>().Result;
    }
}
