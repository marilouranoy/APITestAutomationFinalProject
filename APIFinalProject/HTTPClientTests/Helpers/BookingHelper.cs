using HTTPClientTests.DataModels;
using HTTPClientTests.Resources;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HTTPClientTests.Helpers
{
    public class BookingHelper
    {
        /*public async Task<HttpResponseMessage> SendAsyncFunction(HttpClient HttpClient, HttpMethod method, string url, BookingModel? bookingData = null)
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
        }*/

        public async Task<HttpResponseMessage> SendAsyncFunction(HttpClient HttpClient, HttpMethod method, string url, BookingModel bookingData = null, BookingResponseModel bookingResponseData = null)
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

            if (bookingResponseData != null)
            {
                // Serialize Content
                var request = JsonConvert.SerializeObject(bookingResponseData);
                httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            var httpResponse = await HttpClient.SendAsync(httpRequestMessage);

            return httpResponse;
        }

        /*public async Task<HttpResponseMessage> GetAsyncFunction(HttpClient HttpClient, HttpMethod method, string url, BookingResponseModel bookingResponseData = null)
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
        }*/

        public async Task<HttpResponseMessage> PutAsyncFunction(HttpClient HttpClient, HttpMethod method, string url, BookingModel bookingData = null)
        {
            // Initialize Request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            var userName = "admin";
            var userPassword = "password123";

            var authenticationString = $"{userName}:{userPassword}";
            var base64String = Convert.ToBase64String(
               System.Text.Encoding.ASCII.GetBytes(authenticationString));

            httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://restful-booker.herokuapp.com/auth");
            httpRequestMessage.Headers.Authorization =
               new AuthenticationHeaderValue("Basic", base64String);

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
    }
}
