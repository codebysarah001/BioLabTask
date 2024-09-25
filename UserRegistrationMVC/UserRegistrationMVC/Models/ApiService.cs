using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationMVC.Models
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public void SetBasicAuthentication(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<HttpResponseMessage> GetProtectedData(string apiUrl)
        {
            return await _httpClient.GetAsync(apiUrl);
        }
    }
}