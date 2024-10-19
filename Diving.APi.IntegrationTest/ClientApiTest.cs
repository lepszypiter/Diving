namespace Diving.APi.IntegrationTest;

[TestCaseOrderer(
    ordererTypeName: "Diving.APi.IntegrationTest.PriorityOrderer",
    ordererAssemblyName: "Diving.APi.IntegrationTest")]
public class ClientApiTest
{
    private readonly HttpClient _client = new();
    private static readonly Fixture Fixture = new();

    private static long _testClientId;

    public record ReadClientsDto(long ClientId, string Name, string Surname, string Email);
    public record NewClientRequest(string Name, string Surname, string Email);

    public ClientApiTest()
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
    [TestPriority(3)]
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
    [TestPriority(2)]
    public async Task ReadTestWithId()
    {
        var result = await GetClientWithIdAsync("/api/Client",_testClientId);
        result.Should().NotBeNull();
        var client = result;
        client!.ClientId.Should().BeGreaterThan(0);
        client.Name.Should().NotBeNullOrEmpty();
        client.Surname.Should().NotBeNullOrEmpty();
        client.Email.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [TestPriority(1)]
    public async Task PostTest()
    {
        var newClientRequest = CreateRandomNewClientRequest();
        var result = await PostClientAsync(newClientRequest);
        result.Should().NotBeNull();
        result!.ClientId.Should().BeGreaterThan(0);
        result.Should().BeEquivalentTo(newClientRequest, x => x.ExcludingMissingMembers());
        _testClientId = result.ClientId;
    }

    [Fact]
    [TestPriority(4)]
    public async Task DeleteTest()
    {
        var result = await DeleteClientAsync(_testClientId);
        result.Should().BeTrue();
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

    private async Task<ReadClientsDto?> GetClientWithIdAsync(string path, long id)
        {
            path += $"/{id}";
            var response = await _client.GetAsync(path);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsAsync<ReadClientsDto>();
        }

    private async Task<ReadClientsDto?> PostClientAsync(NewClientRequest fakeClient)
    {
        var response = await _client.PostAsJsonAsync(
            "/api/Client", fakeClient);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return response.Content.ReadAsAsync<ReadClientsDto>().Result;
    }

    private async Task<bool> DeleteClientAsync(long id)
    {
        var response = await _client.DeleteAsync(
            $"/api/Client/{id}");
        return response.IsSuccessStatusCode;
    }

    private static NewClientRequest CreateRandomNewClientRequest()
    {
        return new(
            Fixture.Create<string>(),
            Fixture.Create<string>(),
            "aa@aa.pl");
    }
}
