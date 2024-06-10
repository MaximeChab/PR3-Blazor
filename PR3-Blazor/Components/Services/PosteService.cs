using Newtonsoft.Json;
using PR3_Blazor.Components.Models;
using System.Text;

namespace PR3_Blazor.Components.Services
{
    public class PosteService
{
        private readonly HttpClient _httpClient;

        public PosteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Poste>> GetAllPoste()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5252/api/Postes");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Poste>>(data);
        }

        public async Task<Poste> GetPosteById(int posteId)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5252/api/Postes/{posteId}");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Poste>(data);

        }

        public async Task AddPoste(Poste poste)
        {
            var posteFormated = JsonConvert.SerializeObject(poste);
            var content = new StringContent(posteFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5252/api/Postes", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdatePoste(Poste poste)
        {
            var posteFormated = JsonConvert.SerializeObject(poste);
            var content = new StringContent(posteFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5252/api/Postes/{poste.Id}", content);
            response.EnsureSuccessStatusCode();

        }

        public async Task DeletePoste(int posteId)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5252/api/Postes/{posteId}");
            response.EnsureSuccessStatusCode();

        }
    }
}
