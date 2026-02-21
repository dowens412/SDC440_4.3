using System.Net.Http.Headers;
using System.Text;

namespace MauiBasicAuth_David_Owens.DataAccess;

public class UserAuthentication
{
    
    private const int ApiPort = 5049;

        private static string GetBaseUrl()
    {
#if ANDROID
        return $"http://10.0.2.2:{ApiPort}/";
#else
        return $"http://localhost:{ApiPort}/";
#endif
    }

    public async Task<bool> AuthenticateUser(string username, string password)
    {
        using var client = new HttpClient
        {
            BaseAddress = new Uri(GetBaseUrl())
        };

        var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

        using var response = await client.GetAsync("api/values");

        return response.IsSuccessStatusCode;
    }
}