using Newtonsoft.Json;
using PR3_Blazor.Components.Models;
using System.Text;

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
    }
}

