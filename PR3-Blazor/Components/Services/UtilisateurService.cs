using Newtonsoft.Json;
using PR3_Blazor.Components.Models;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace PR3_Blazor.Components.Services
{
    public class UtilisateurService
{
        private readonly HttpClient _httpClient;

        public UtilisateurService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Utilisateur>> GetAllUtilisateur()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5252/api/Utilisateurs");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Utilisateur>>(data);
        }

        public async Task<Utilisateur> GetUtilisateurById(int utilisateurId)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5252/api/Utilisateurs/{utilisateurId}");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Utilisateur>(data);

        }

        public async Task AddUtilisateur(Utilisateur utilisateur)
        {
            var UtilisateurFormated = JsonConvert.SerializeObject(utilisateur);
            var content = new StringContent(UtilisateurFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5252/api/Utilisateurs/Create", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUtilisateur(Utilisateur utilisateur)
        {
            var UtilisateurFormated = JsonConvert.SerializeObject(utilisateur);
            var content = new StringContent(UtilisateurFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5252/api/Utilisateurs/{utilisateur.Id}", content);
            response.EnsureSuccessStatusCode();

        }

        public async Task DeleteUtilisateur(int utilisateurId)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5252/api/Utilisateurs/{utilisateurId}");
            response.EnsureSuccessStatusCode();

        }

        public async Task<string> UseExist(LoginRequest loginRequest)
        {
            loginRequest.Login = "admin";
            loginRequest.MotDePasse = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";

            var response = await _httpClient.PostAsJsonAsync("api/Utilisateurs/Exist", loginRequest);

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

