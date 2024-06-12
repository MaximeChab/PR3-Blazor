using Microsoft.JSInterop;
using static PR3_Blazor.Components.Services.UtilisateurService;

namespace PR3_Blazor.Components.Services
{
    public class AuthService
{
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;

        }
        public async Task<string> GetToken()
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5011/Auth", new { });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<JwtResponse>();
            return result.Token;
        }
    }
}
