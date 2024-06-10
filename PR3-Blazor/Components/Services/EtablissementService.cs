using Newtonsoft.Json;
using PR3_Blazor.Components.Models;
using System.Text;

namespace PR3_Blazor.Components.Services
{
    public class EtablissementService
{
        private readonly HttpClient _httpClient;

        public EtablissementService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Etablissement>> GetAllEtablissement()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5252/api/Etablissements");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Etablissement>>(data);
        }

        public async Task<Etablissement> GetEtablissementById(int etablissementId)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5252/api/Etablissements/{etablissementId}");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Etablissement>(data);

        }

        public async Task AddEtablissement(Etablissement etablissement)
        {
            var etablissementFormated = JsonConvert.SerializeObject(etablissement);
            var content = new StringContent(etablissementFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5252/api/Etablissements", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEtablissement(Etablissement etablissement)
        {
            var etablissementFormated = JsonConvert.SerializeObject(etablissement);
            var content = new StringContent(etablissementFormated, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5252/api/Etablissements/{etablissement.Id}", content);
            response.EnsureSuccessStatusCode();

        }

        public async Task DeleteEtablissement(int etablissementId)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5252/api/Etablissements/{etablissementId}");
            response.EnsureSuccessStatusCode();

        }
    }
}
