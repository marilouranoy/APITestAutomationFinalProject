using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpTests.DataModels;

namespace RestSharpTests.Tests
{
    public class APIBaseTests
    {
        public RestClient RestClient { get; set; }
        public BookingModel BookingDetails { get; set; }

        //test initialize method which is first called before any other methods
        [TestInitialize]
        public void Initialize()
        {
            RestClient = new RestClient();
        }

        //test cleanup method which is called before ending our test
        [TestCleanup]
        public void CleanUp()
        {

        }
    }
}
