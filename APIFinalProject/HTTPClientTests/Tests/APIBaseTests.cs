using HTTPClientTests.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HTTPClientTests.Tests
{
    public class APIBaseTests
    {
        public HttpClient HttpClient { get; set; }
        public BookingModel BookingDetails { get; set; }

        public readonly List<BookingResponseModel> CleanUpList = new List<BookingResponseModel>();


        //test initialize method which is first called before any other methods

        [TestInitialize]
        public void Initialize()
        {
            HttpClient = new HttpClient();
        }

        //test cleanup method which is called before ending our test
        [TestCleanup]
        public void CleanUp()
        {
        }
    }
}
