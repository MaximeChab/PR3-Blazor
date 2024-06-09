using PR3_Blazor.Components.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;

namespace PR3_Blazor.Components.Services
{
    public class SalleService

    {
        private readonly HttpClient _httpClient;

        public SalleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Salle>> GetAllSalle()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5252/api/Salles");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Salle>>(data);
        }

        /**public async Task<List<Salle>> GetAllSalleByEtablissment()
        {
        }**/

        public async Task<Salle> GetSalleById(int salleId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5252/api/Salles/{salleId}");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salle>(data);
        }

        public async Task AddSalle(Salle salle)
        {
            var salleFormated = JsonConvert.SerializeObject(salle);
            var content = new StringContent(salleFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5252/api/Salles", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSalle(Salle salle)
        {
            var salleFormated = JsonConvert.SerializeObject(salle);
            var content = new StringContent(salleFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("http://localhost:5252/api/Salles/{salle.id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSalle(int salleId)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5252/api/Salles/{salleId}");
            response.EnsureSuccessStatusCode();

        }
    }

}
