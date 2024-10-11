using AutoFixture;

namespace Diving.APi.IntegrationTest;

public class ClientApiTest
{
    private readonly HttpClient _client = new();
    private static readonly Fixture Fixture = new();

    private const int FakeId = 1;

    public record ReadClientsDto(long ClientId, string Name, string Surname, string Email);
    public record AddClientCommand(string Name, string Surname, string Email);

    public ClientApiTest()
    {
        _client.BaseAddress = new Uri("http://localhost:5175");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    [Fact]
    public async Task ReadTest()
    {
        var result = await GetClientsAsync("/api/Client");
        result.Should().NotBeNull();
        result!.Length.Should().BeGreaterThan(0);
        var client = result[0];
        client.ClientId.Should().BeGreaterThan(0);
        client.Name.Should().NotBeNullOrEmpty();
        client.Surname.Should().NotBeNullOrEmpty();
        client.Email.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ReadTestWithId()
    {
        var result = await GetClientWithIdAsync("/api/Client",FakeId);
        result.Should().NotBeNull();
        var client = result;
        client!.ClientId.Should().BeGreaterThan(0);
        client.Name.Should().NotBeNullOrEmpty();
        client.Surname.Should().NotBeNullOrEmpty();
        client.Email.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task PostTest()
    {
        var fakeClient = CreateFakeAddClientCommand();
        var result = await PostClientAsync(fakeClient);
        result.Should().NotBeNull();
        result!.ClientId.Should().BeGreaterThan(0);
        result.Should().BeEquivalentTo(fakeClient, x => x.ExcludingMissingMembers());
    }

    [Fact]
    public async Task DeleteTest()
    {
        var result = await DeleteClientAsync(FakeId);
        result.Should().NotBeNull();
        //result!.ClientId.Should().Be(204);
    }

    private async Task<ReadClientsDto[]?> GetClientsAsync(string path)
        {
            var response = await _client.GetAsync(path);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsAsync<ReadClientsDto[]>();
        }

    private async Task<ReadClientsDto?> GetClientWithIdAsync(string path, int id)
        {
            path += $"/{id}";
            var response = await _client.GetAsync(path);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsAsync<ReadClientsDto>();
        }

    private async Task<ReadClientsDto?> PostClientAsync(AddClientCommand fakeClient)
    {
        var response = await _client.PostAsJsonAsync(
            "/api/Client", fakeClient);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return response.Content.ReadAsAsync<ReadClientsDto>().Result;
    }

    private async Task<ReadClientsDto?> DeleteClientAsync(int id)
    {
        var response = await _client.DeleteAsync(
            $"/api/Client/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return response.Content.ReadAsAsync<ReadClientsDto>().Result;
    }

    private static AddClientCommand CreateFakeAddClientCommand()
    {
        return new(
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            "aa@aa.pl");
    }
}
