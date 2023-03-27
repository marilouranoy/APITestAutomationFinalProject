using HTTPClientTests.DataModels;
using HTTPClientTests.Resources;
using Newtonsoft.Json;
using System.Text;

namespace HTTPClientTests.Helpers
{
    public class BookingHelper
    {
        public async Task<HttpResponseMessage> SendAsyncFunction(HttpClient HttpClient, HttpMethod method, string url, BookingModel bookingData = null)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Method = method;
            httpRequestMessage.RequestUri = Endpoints.GetURI(url);
            httpRequestMessage.Headers.Add("Accept", "application/json");

            if (bookingData != null)
            {
                // Serialize Content
                var request = JsonConvert.SerializeObject(bookingData);
                httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            var httpResponse = await HttpClient.SendAsync(httpRequestMessage);

            return httpResponse;
        }

        public async Task<HttpResponseMessage> GetAsyncFunction(HttpClient HttpClient, HttpMethod method, string url, BookingResponseModel bookingResponseData = null)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Method = method;
            httpRequestMessage.RequestUri = Endpoints.GetURI(url);
            httpRequestMessage.Headers.Add("Accept", "application/json");

            if (bookingResponseData != null)
            {
                // Serialize Content
                var request = JsonConvert.SerializeObject(bookingResponseData);
                httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            var httpResponse = await HttpClient.SendAsync(httpRequestMessage);

            return httpResponse;
        }
    }
}
