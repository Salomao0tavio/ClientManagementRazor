using B2B.Models.Models;
using B2B_Web.Services;

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient;

    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Login(UserDTO model)
    {

        var response = await _httpClient.PostAsJsonAsync("/v1/login", model);

        var content = await response.Content.ReadAsStringAsync();

        return content;

    }
}