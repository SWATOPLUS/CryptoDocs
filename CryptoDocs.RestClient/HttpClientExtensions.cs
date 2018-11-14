using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CryptoDocs.RestClient
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri)
        {
            var responseJson = await httpClient.GetStringAsync(requestUri);
            return Json.Deserialize<T>(responseJson);
        }

        public static Task PostJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Post, requestUri, content);

        public static Task<T> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content);

        public static Task PutJsonAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync(HttpMethod.Put, requestUri, content);

        public static Task<T> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content);

        public static Task SendJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri,
            object content)
            => httpClient.SendJsonAsync<IgnoreResponse>(method, requestUri, content);

        public static async Task<T> SendJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri,
            object content)
        {
            var requestJson = Json.Serialize(content);
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            });

            if (typeof(T) == typeof(IgnoreResponse))
            {
                return default(T);
            }
            else
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return Json.Deserialize<T>(responseJson);
            }
        }

        private class IgnoreResponse
        {
        }
    }
}
