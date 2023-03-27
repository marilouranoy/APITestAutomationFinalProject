using RestSharpTests.DataModels;

namespace RestSharpTests.Tests.TestData
{
    public class GenerateBooking
    {
        public static BookingModel bookingData()
        {
            return new BookingModel
            {
                Firstname = "TestFirstName",
                Lastname = "TestLastName",
                Totalprice = 600,
                Depositpaid = true,
                Bookingdates = new BookingDatesModel()
                {
                    Checkin = DateTime.Parse("2023-03-28"),
                    Checkout = DateTime.Parse("2023-03-30")
                },
                Additionalneeds = "Snacks"
            };
        }
    }
}