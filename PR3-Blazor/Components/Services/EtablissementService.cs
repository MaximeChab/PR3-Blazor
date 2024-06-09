using Newtonsoft.Json;
using PR3_Blazor.Components.Models;

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
    }
}
