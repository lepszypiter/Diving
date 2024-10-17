namespace Diving.APi.IntegrationTest;

public class AuthenticationClient
{
    private static string? _tokenCache;

    private record Token(string AccessToken);

    public async Task<string> GetToken()
    {
        if (_tokenCache != null)
        {
            return _tokenCache;
        }

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://dev-diving.eu.auth0.com/oauth/token");
        //request.Headers.Add("content-type", "application/x-www-form-urlencoded");

        //Debug.Assert(request.Content != null, "request.Content != null");
        //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        var collection = new List<KeyValuePair<string, string>>();
        collection.Add(new("grant_type", "client_credentials"));
        collection.Add(new("client_id", "FHDSlxT0xdRrxw32o5ruRWDbtwM32Yje"));
        collection.Add(new("client_secret", "DdaapM-ueAdJz9ichkNSuiGIdV_vOcntk-XVkSrYegmi7no0otyYhQpbaudzr2Pg"));
        collection.Add(new("audience", "https://quickstarts/api"));
        request.Content = new FormUrlEncodedContent(collection);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        var readAsAsync = await response.Content.ReadAsAsync<Token>();
        Console.WriteLine(readAsAsync);
        _tokenCache = "Bearer " + readAsAsync.AccessToken;
        return _tokenCache;
    }
}
