using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoapAPITests.Tests
{
    [TestClass]
    public class CountryInfoServiceTests
    {
        //Declare an instance of the CountryInfoServiceSoapTypeClient to be used in making connection and calling the services
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoServiceTest =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);


        private ServiceReference1.tCountryCodeAndName[] GetListOfCountryNamesByCode()
        {
            return countryInfoServiceTest.ListOfCountryNamesByCode();
        }

        private ServiceReference1.tCountryCodeAndName GetRandomRecord(ref ServiceReference1.tCountryCodeAndName[] countryCodeAndNames)
        {
            var random = new Random();
            var listCountryCodeAndNames = countryCodeAndNames.ToList();
            int randomNumber = random.Next(listCountryCodeAndNames.Count);
            return countryCodeAndNames[randomNumber];
        }
        /// <summary>
        /// Test method TestFullCountryInfo
        /// - 
        /// </summary>
        [TestCategory("SOAP API Tests")]
        [TestMethod]
        public void TestFullCountryInfo()
        {
            ServiceReference1.tCountryCodeAndName[] listCountryCodeAndNames = GetListOfCountryNamesByCode();
            ServiceReference1.tCountryCodeAndName returnRandomRecord = GetRandomRecord(ref listCountryCodeAndNames);

            var returnFullCountryInfo = countryInfoServiceTest.FullCountryInfo(returnRandomRecord.sISOCode);

            Assert.AreEqual(returnRandomRecord.sISOCode, returnFullCountryInfo.sISOCode);
            Assert.AreEqual(returnRandomRecord.sName, returnFullCountryInfo.sName);

            //create another list that sorts the returned list above by ascending order using the country code
            //var sortedListCountryCodes = returnListCountryCodes.OrderBy(s => s.sISOCode);
            //use collection assert to check if our lists (original and sorted) are equal
            //CollectionAssert.AreEqual(sortedListCountryCodes.ToList(), returnListCountryCodes.ToList());
        }

        [TestCategory("SOAP API Tests")]
        [TestMethod]
        public void TestCountryISOCode()
        {
            ServiceReference1.tCountryCodeAndName returnRandomRecord;
            ServiceReference1.tCountryCodeAndName[] listCountryCodeAndNames = GetListOfCountryNamesByCode();

            for(int i = 0; i < 5; i++)
            {
                returnRandomRecord = GetRandomRecord(ref listCountryCodeAndNames);
                string countryCode = countryInfoServiceTest.CountryISOCode(returnRandomRecord.sName);
                Assert.AreEqual(returnRandomRecord.sISOCode, countryCode);
            }
        }
    }
}