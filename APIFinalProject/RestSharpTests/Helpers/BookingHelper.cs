using RestSharp;
using RestSharpTests.DataModels;
using RestSharpTests.Resources;
using RestSharpTests.Tests.TestData;

namespace RestSharpTests.Helpers
{
    public class BookingHelper
    {
        public static BookingResponseModel? BookingResponseDetails { get; private set; }
        public static BookingModel? BookingDetails { get; private set; }

        public static BookingModel? RetrievedBookingDetails { get; private set; }

        /// <summary>
        /// Method for adding new booking using Post request
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<RestResponse> CreateNewBooking(RestClient client, BookingModel bookingData)
        {
            var postRequest = new RestRequest(Endpoints.CreateBooking());

            postRequest.AddJsonBody(bookingData);
            postRequest.AddHeader("Accept", "application/json");
            var postResponse = await client.ExecutePostAsync<BookingResponseModel>(postRequest);
            BookingResponseDetails = postResponse.Data;
            return postResponse;
        }

        /// <summary>
        /// Method for getting booking data using Get request and with parameter Booking Id
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public static async Task<RestResponse> GetBookingDataById(RestClient client, int bookingId)
        {
            var getRequest = new RestRequest(Endpoints.GetBookingById(bookingId));
            getRequest.AddHeader("Accept", "application/json");
            var getResponse = await client.ExecuteGetAsync<BookingModel>(getRequest);

            RetrievedBookingDetails = getResponse.Data;
            return getResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public static async Task<RestResponse> UpdateBookingDataById(RestClient client, BookingModel updatedBookingData, int bookingId)
        {
            #region get token data
            // Initialize Request
            var authenticationURL = "https://restful-booker.herokuapp.com/auth";
            var authenticationData = "{\"username\":\"admin\",\"password\":\"password123\"}";

            // Send Post Request
            var restRequest = new RestRequest(authenticationURL).AddStringBody(authenticationData, DataFormat.Json);
            var restResponse = await client.ExecutePostAsync<TokenModel>(restRequest);

            // Get Token Data
            var token = restResponse.Data.Token;
            #endregion


            var updateRequest = new RestRequest(Endpoints.UpdateBookingDataById(bookingId));
            updateRequest.AddJsonBody(updatedBookingData);
            updateRequest.AddHeader("Accept", "application/json");
            updateRequest.AddHeader("Cookie", $"token={token}");
            var getResponse = await client.ExecutePutAsync<BookingModel>(updateRequest);

            BookingDetails = getResponse.Data;
            return getResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public static async Task<RestResponse> DeleteBookingById(RestClient client, int bookingId)
        {
            #region get token data
            // Initialize Request
            var authenticationURL = "https://restful-booker.herokuapp.com/auth";
            var authenticationData = "{\"username\":\"admin\",\"password\":\"password123\"}";

            // Send Post Request
            var restRequest = new RestRequest(authenticationURL).AddStringBody(authenticationData, DataFormat.Json);
            var restResponse = await client.ExecutePostAsync<TokenModel>(restRequest);

            // Get Token Data
            var token = restResponse.Data.Token;
            #endregion

            var deleteRequest = new RestRequest(Endpoints.DeleteBookingDataById(bookingId));
            deleteRequest.AddHeader("Accept", "application/json");
            deleteRequest.AddHeader("Cookie", $"token={token}");
            var deleteResponse = await client.DeleteAsync(deleteRequest);
            return deleteResponse;
        }
    }
}
