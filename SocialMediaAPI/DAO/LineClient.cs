using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialMediaAPI.DAO
{
    public interface ILineClient
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);
        Task<T> PostAsync<T>(string accessToken, string endpoint, object data, string mediaType = "application/json" , string args = null);

        Task<T> PosturlencodedAsync<T>(string accessToken, string endpoint, List<KeyValuePair<string, string>> data, string mediaType = "application/json", string args = null);

    }

    public class LineClient : ILineClient
    {
        private readonly HttpClient _httpClient;

        public LineClient(string BaseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

     


        public async Task<T> GetAsync<T>(string BearerToken, string endpoint, string args = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            var response = await _httpClient.GetAsync($"{endpoint}?{args}");
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<T> PostAsync<T>(string BearerToken, string endpoint, object data, string mediaType = "application/json", string args = null)
        {
            if(BearerToken != "" || BearerToken != null)  
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

            }

            var payload = GetPayload(data, mediaType);
            var response =  await _httpClient.PostAsync($"{endpoint}?{args}", payload);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(result);
          


            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<T> PosturlencodedAsync<T>(string BearerToken, string endpoint, List<KeyValuePair<string, string>> data, string mediaType = "application/json", string args = null)
        {
            if (BearerToken != "" || BearerToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

            }

            var payload = GetPayload(data, mediaType);
            var req = new HttpRequestMessage(HttpMethod.Post, $"{endpoint}?{args}") { Content = new FormUrlEncodedContent(data) };

            var response = await _httpClient.SendAsync(req);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(result);



            return JsonConvert.DeserializeObject<T>(result);
        }

        private static StringContent GetPayload(object data , string mediaType = "application/json")
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, mediaType);
        }
    }
}