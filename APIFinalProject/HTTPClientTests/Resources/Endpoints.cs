using HTTPClientTests.DataModels;

namespace HTTPClientTests.Resources
{
    public class Endpoints
    {
        public static readonly string BaseURL = "https://restful-booker.herokuapp.com";
        public static readonly string BookingEndpoint = "/booking";
        public static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";
        public static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        public static string GetBookingById(int bookingId) => $"{BookingEndpoint}/{bookingId}";
        public static string UpdateBookingDataById(int bookingId) => $"{BookingEndpoint}/{bookingId}";
        public static string DeleteBookingDataById(int bookingId) => $"{BookingEndpoint}/{bookingId}";

    }
}
