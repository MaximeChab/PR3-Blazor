using Newtonsoft.Json;
using PR3_Blazor.Components.Models;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.JSInterop;
using System.Net.Http.Headers;


namespace PR3_Blazor.Components.Services
{
    public class UtilisateurService
{
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public UtilisateurService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;

        }
        public async Task<List<Utilisateur>> GetAllUtilisateur()
        {
            string token = await GetTokenFromSessionAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5252/api/Utilisateurs");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Utilisateur>>(data);
        }

        public async Task<Utilisateur> GetUtilisateurById(int utilisateurId)
        {
            string token = await GetTokenFromSessionAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5252/api/Utilisateurs/{utilisateurId}");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Utilisateur>(data);

        }

        public async Task AddUtilisateur(Utilisateur utilisateur) 
        {
            string token = await GetTokenFromSessionAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var UtilisateurFormated = JsonConvert.SerializeObject(utilisateur);
            var content = new StringContent(UtilisateurFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5252/api/Utilisateurs/Create", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUtilisateur(Utilisateur utilisateur)
        {
            string token = await GetTokenFromSessionAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var UtilisateurFormated = JsonConvert.SerializeObject(utilisateur);
            var content = new StringContent(UtilisateurFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5252/api/Utilisateurs/{utilisateur.Id}", content);
            response.EnsureSuccessStatusCode();

        }

        public async Task DeleteUtilisateur(int utilisateurId)
        {
            string token = await GetTokenFromSessionAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"http://localhost:5252/api/Utilisateurs/{utilisateurId}");
            response.EnsureSuccessStatusCode();

        }

        public async Task<string> UserExist(string login, string motDePasse)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Login = "admin";
            loginRequest.MotDePasse = "admin";

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5252/api/Utilisateurs/Exist", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JwtResponse>();
                // Optionally store the token, e.g., in local storage or a service
                // await _localStorage.SetItemAsync("authToken", result.Token);
                return result.Token;
            }
            else
            {
                // Handle the case where the user does not exist or other errors
                // You might want to throw an exception or return a specific result
                return null;
            }
        }


        public async Task<string> GetTokenFromSessionAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        }


        public class LoginRequest
        {
            public string Login { get; set; }
            public string MotDePasse { get; set; }
        }

        public class JwtResponse
        {
            public string Token { get; set; }
        }
    }
}

