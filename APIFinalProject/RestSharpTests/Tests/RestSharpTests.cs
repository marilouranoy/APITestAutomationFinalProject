using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpTests.DataModels;
using RestSharp;
using RestSharpTests.Resources;
using System.Net;
using RestSharpTests.Helpers;
using RestSharpTests.Tests.TestData;
using Newtonsoft.Json;

namespace RestSharpTests.Tests
{
    [TestClass]
    public class RestSharpTests : APIBaseTests
    {
        //define our cleanup list
        private static List<BookingResponseModel> bookingResponseCleanUpList = new List<BookingResponseModel>();

        //test initialize method which also gets called after the main test initialize method in our base class
        [TestInitialize]
        public async Task TestInitialize()
        {
        }

        //test clean up method where we clean up/delete the test data that we used in our Post request
        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var bookingData in bookingResponseCleanUpList)
            {
                var deleteBookingRequest = new RestRequest(Endpoints.DeleteBookingDataById(bookingData.BookingId));
                var deleteBookingResponse = await BookingHelper.DeleteBookingById(RestClient, bookingData.BookingId);
            }
        }

        //test method for Get pet request
        [TestMethod]
        [TestCategory("RestSharp Tests")]
        public async Task TestCreateBooking()
        {

            BookingModel newBookingData = GenerateBooking.bookingData();

            var bookingResponse = await BookingHelper.CreateNewBooking(RestClient, newBookingData);

            bookingResponseCleanUpList.Add(BookingHelper.BookingResponseDetails);

            int bookingId = BookingHelper.BookingResponseDetails.BookingId;

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, bookingResponse.StatusCode, "Status code is not equal to 200");

            var getBookingResponse = await BookingHelper.GetBookingDataById(RestClient, bookingId);
            var retrievedBookingData = BookingHelper.RetrievedBookingDetails;
            Assert.AreEqual(JsonConvert.SerializeObject(newBookingData), JsonConvert.SerializeObject(retrievedBookingData), "Booking data are not the same.");

        }

        
        [TestMethod]
        [TestCategory("RestSharp Tests")]
        public async Task TestUpdateBooking()
        {

            BookingModel newBookingData = GenerateBooking.bookingData();

            var createBookingResponse = await BookingHelper.CreateNewBooking(RestClient, newBookingData);

            bookingResponseCleanUpList.Add(BookingHelper.BookingResponseDetails);

            int bookingId = BookingHelper.BookingResponseDetails.BookingId;

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, createBookingResponse.StatusCode, "Status code is not equal to 200");

            //Update the value of first and last name of booking data
            newBookingData.Firstname = "TestUpdatedFirstName";
            newBookingData.Lastname = "TestUpdatedLastName";

            var updateBookingResponse = await BookingHelper.UpdateBookingDataById(RestClient, newBookingData, bookingId);
            //Verify PUT request status code
            Assert.AreEqual(HttpStatusCode.OK, updateBookingResponse.StatusCode, "Status code is not equal to 200");

            var getBookingResponse = await BookingHelper.GetBookingDataById(RestClient, bookingId);
            var retrievedBookingData = BookingHelper.RetrievedBookingDetails;
            Assert.AreEqual(JsonConvert.SerializeObject(newBookingData), JsonConvert.SerializeObject(retrievedBookingData), "Booking data are not the same after the update.");
        }

        [TestMethod]
        [TestCategory("RestSharp Tests")]
        public async Task TestCreateAndDeleteBooking()
        {

            BookingModel newBookingData = GenerateBooking.bookingData();

            var createBookingResponse = await BookingHelper.CreateNewBooking(RestClient, newBookingData);

            int bookingId = BookingHelper.BookingResponseDetails.BookingId;

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, createBookingResponse.StatusCode, "Status code is not equal to 200");

            //delete the booking created
            var deleteBookingResponse = await BookingHelper.DeleteBookingById(RestClient, bookingId);

            //execute Get request and deserialize the response to be used for comparison later
            var statusCode = deleteBookingResponse.StatusCode;
            Assert.AreEqual(HttpStatusCode.Created, statusCode, "Status code is not equal to 201 (Created).");

        }

        //test method for Get pet request
        [TestMethod]
        [TestCategory("RestSharp Tests")]
        public async Task TestGetInvalidBookingId()
        {
            int invalidBookingId = -1;//

            var getRequest = new RestRequest(Endpoints.GetBookingById(invalidBookingId));
            getRequest.AddHeader("Accept", "application/json");

            //Act - we call the corresponding operation/method for our API test
            var getBookingResponse = await BookingHelper.GetBookingDataById(RestClient, invalidBookingId);

            //execute Get request and deserialize the response to be used for comparison later
            var statusCode = getBookingResponse.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode, "Status code is not equal to 401 (Not Found).");
        }
    }
}