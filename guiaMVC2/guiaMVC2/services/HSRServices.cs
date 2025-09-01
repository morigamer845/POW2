using guiaMVC2.Models;
using Newtonsoft.Json;
using System.Net.Http;
namespace guiaMVC2.services
{
    public class HSRServices
    {
        private readonly HttpClient _httpClient;

        public HSRServices()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://hsr-api.vercel.app/api/v1/characters"); // base de la API
        }

        public async Task<List<Personaje>> GetCharactersAsync()
        {
            var response = await _httpClient.GetAsync("characters");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var personaje = JsonConvert.DeserializeObject<List<Personaje>>(json);

            return personaje;
        }
    }
}
