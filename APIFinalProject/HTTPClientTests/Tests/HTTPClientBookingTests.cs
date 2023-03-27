using HTTPClientTests.Tests.TestData;
using HTTPClientTests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using HTTPClientTests.DataModels;
using HTTPClientTests.Helpers;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HTTPClientTests.Tests
{
    [TestClass]
    public class HTTPClientBookingTests : APIBaseTests
    {
        [TestCategory("HTTP Client Tests")]
        [TestMethod]
        public async Task TestPostBooking()
        {
            /*
            #region get token data
            // Initialize Request
            var authenticationURL = "https://restful-booker.herokuapp.com/auth";
            var authenticationData = "{\"username\":\"admin\",\"password\":\"password123\"}";

            // Send Post Request
            var httpRequest = JsonConvert.SerializeObject(newBookingData);
            var httpResponse = await restClient.ExecutePostAsync<TokenModel>(restRequest);
            // Validate Reponse Status Code
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode, "Status Code is not OK");

            // Get Token Data
            var token = httpResponse.Data.Token;
            #endregion
            */

            var newBookingData = GenerateBooking.bookingData();


            BookingHelper bookingHelper = new BookingHelper();
            var httpResponse = await bookingHelper.SendAsyncFunction(HttpClient, HttpMethod.Post, Endpoints.BookingEndpoint, newBookingData);
            var statusCode = httpResponse.StatusCode;
            var httpResponseMessage = httpResponse.Content;
            var bookingResponseModel = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponseMessage.ReadAsStringAsync().Result);
 
            // Add data to cleanup list
            CleanUpList.Add(bookingResponseModel);

            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200 (OK).");

            //execute Get request and deserialize the response to be used for comparison later
            var getResponse = await bookingHelper.GetAsyncFunction(HttpClient, HttpMethod.Get,$"{Endpoints.BookingEndpoint}/{bookingResponseModel.BookingId}", bookingResponseModel);
            var responseBookingData = JsonConvert.DeserializeObject<BookingModel>(getResponse.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(JsonConvert.SerializeObject(newBookingData), JsonConvert.SerializeObject(responseBookingData), "Booking data are not the same.");

        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var cleanUp in CleanUpList)
            {
               // var httpResponse = await HttpClient.DeleteAsync(Endpoints.GetURL($"{Endpoints.BookingEndpoint}/{cleanUp.BookingId}"));
            }
        }
    }
}
