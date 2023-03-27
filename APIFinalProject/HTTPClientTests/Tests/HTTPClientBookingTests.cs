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
        [TestMethod]
        [TestCategory("HTTP Client Tests")]
        public async Task TestCreateBooking()
        {

            var newBookingData = GenerateBooking.bookingData();

            BookingHelper bookingHelper = new BookingHelper();
            var httpResponse = await bookingHelper.SendAsyncFunction(HttpClient, HttpMethod.Post, Endpoints.BookingEndpoint, newBookingData, null);
            var statusCode = httpResponse.StatusCode;
            var httpResponseMessage = httpResponse.Content;
            var bookingResponseModel = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponseMessage.ReadAsStringAsync().Result);
 
            // Add data to cleanup list
            CleanUpList.Add(bookingResponseModel);

            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200 (OK).");

            //execute Get request and deserialize the response to be used for comparison later
            var getResponse = await bookingHelper.SendAsyncFunction(HttpClient, HttpMethod.Get, Endpoints.GetBookingById(bookingResponseModel.BookingId), null, bookingResponseModel);
            var responseBookingData = JsonConvert.DeserializeObject<BookingModel>(getResponse.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(JsonConvert.SerializeObject(newBookingData), JsonConvert.SerializeObject(responseBookingData), "Booking data are not the same.");

        }

        [TestMethod]
        [TestCategory("HTTP Client Tests")]
        public async Task TestUpdateBooking()
        {

            var newBookingData = GenerateBooking.bookingData();

            BookingHelper bookingHelper = new BookingHelper();
            var httpResponse = await bookingHelper.SendAsyncFunction(HttpClient, HttpMethod.Post, Endpoints.BookingEndpoint, newBookingData, null);
            var statusCode = httpResponse.StatusCode;
            var httpResponseMessage = httpResponse.Content;
            var bookingResponseModel = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponseMessage.ReadAsStringAsync().Result);

            // Add data to cleanup list
            CleanUpList.Add(bookingResponseModel);

            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200 (OK).");

            //Update the value of first and last name of booking data
            newBookingData.Firstname = "TestUpdatedFirstName";
            newBookingData.Lastname = "TestUpdatedLastName";

            httpResponse = await bookingHelper.PutAsyncFunction(HttpClient, HttpMethod.Put, Endpoints.UpdateBookingDataById(bookingResponseModel.BookingId), newBookingData);
            statusCode = httpResponse.StatusCode;
            //httpResponseMessage = httpResponse.Content;
            //bookingResponseModel = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponseMessage.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200 (OK).");

            //execute Get request and deserialize the response to be used for comparison later
            var getResponse = await bookingHelper.SendAsyncFunction(HttpClient, HttpMethod.Get, Endpoints.GetBookingById(bookingResponseModel.BookingId), null, bookingResponseModel);
            var responseBookingData = JsonConvert.DeserializeObject<BookingModel>(getResponse.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(JsonConvert.SerializeObject(newBookingData), JsonConvert.SerializeObject(responseBookingData), "Booking data are not the same after update.");

        }

        [TestMethod]
        [TestCategory("HTTP Client Tests")]
        public async Task TestCreateAndDeleteBooking()
        {

            var newBookingData = GenerateBooking.bookingData();

            BookingHelper bookingHelper = new BookingHelper();
            var httpResponse = await bookingHelper.SendAsyncFunction(HttpClient, HttpMethod.Post, Endpoints.BookingEndpoint, newBookingData, null);
            var statusCode = httpResponse.StatusCode;
            var httpResponseMessage = httpResponse.Content;
            var bookingResponseModel = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponseMessage.ReadAsStringAsync().Result);

            // Add data to cleanup list
            CleanUpList.Add(bookingResponseModel);

            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200 (OK).");

            //delete the booking created
            httpResponse = await bookingHelper.PutAsyncFunction(HttpClient, HttpMethod.Delete, Endpoints.DeleteBookingDataById(bookingResponseModel.BookingId), null);

            //execute Get request and deserialize the response to be used for comparison later
            statusCode = httpResponse.StatusCode;
            Assert.AreEqual(HttpStatusCode.Created, statusCode, "Status code is not equal to 201 (Created).");

        }

        [TestMethod]
        [TestCategory("HTTP Client Tests")]
        
        public async Task TestGetInvalidBookingId()
        {
            BookingHelper bookingHelper = new BookingHelper();
            int invalidBookingId = -1;//
            //execute Get request and deserialize the response to be used for comparison later
            var getResponse = await bookingHelper.SendAsyncFunction(HttpClient, HttpMethod.Get, Endpoints.GetBookingById(invalidBookingId), null, null);

            //execute Get request and deserialize the response to be used for comparison later
            var statusCode = getResponse.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode, "Status code is not equal to 401 (Not Found).");

        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            BookingHelper bookingHelper = new BookingHelper();
            foreach (var cleanUpData in CleanUpList)
            {
                var httpResponse = await bookingHelper.PutAsyncFunction(HttpClient, HttpMethod.Delete, Endpoints.DeleteBookingDataById(cleanUpData.BookingId), null);
            }
        }
    }
}
