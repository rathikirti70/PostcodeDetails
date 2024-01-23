using Microsoft.AspNetCore.SignalR.Protocol;
using Moq;
using PostcodeDetails.DataAccess;
using PostcodeDetails.Models;
using PostcodeDetails.Services;
using System.Globalization;

namespace PostCodeDetailsTest
{
    [TestClass]
    public class PostCodeServiceTest
    {
        private readonly Mock<IPostCodeDataAccess> _postCodeDataAccess;
        public PostCodeServiceTest()
        {
            _postCodeDataAccess = new Mock<IPostCodeDataAccess>();
        }

        [TestMethod]
        public void LookupPostcodeTest()
        {
            string postcode = "SW1W 0NY";
            PostCodeDetailParent postcodeDetail = new PostCodeDetailParent()
            {
                Status = 200,
                Result = new PostCodeDetail()
                {
                    Country = "England",
                    Latitude = 51.495373f,
                    Region = "London",
                    ParliamentaryConstituency = "Cities of London and Westminster",
                    AdminDistrict = "Westminster"
                }
            };

            _postCodeDataAccess.Setup(x => x.LookupPostcode(It.IsAny<string>())).Returns(postcodeDetail);

            PostCodeService common = new PostCodeService(_postCodeDataAccess.Object);
            var details = common.LookupPostcode(postcode);
            Assert.IsNotNull(details);
        }

        [TestMethod]
        public void LookupPostcodeCatch()
        {
            string postcode = " 0NY";
            PostCodeDetailParent postcodeDetail = new PostCodeDetailParent()
            {
                Status = 404,
                Error = "Invalid postcode",
            };

            _postCodeDataAccess.Setup(x => x.LookupPostcode(It.IsAny<string>())).Returns(postcodeDetail);

            try
            {
                var common = new PostCodeService(_postCodeDataAccess.Object);
                common.LookupPostcode(postcode);

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ApplicationException);
            }

        }

        [TestMethod]
        public void AutocompletePostcodeTest()
        {
            string postcode = "SW1W 0NY";
            AutoCompleteParent autoCompleteParent = new AutoCompleteParent()
            {
                Status = 200,
                Result = new Dictionary<string, string>
                {
                     { "SW10 0AA","South" },
                     { "SW10 0AD","South" },
                     { "SW10 0AE","South" },
                     { "SW10 0AF","South" },
                     { "SW10 0AB","South" },
                     { "SW10 0AG","South" },
                     { "SW10 0AH","South" },
                     { "SW10 0AJ","South" },
                     { "SW10 0AL","South" },
                     { "SW10 0AN","South" }
                }
            };
            _postCodeDataAccess.Setup(x => x.AutocompletePostcode(It.IsAny<string>())).Returns(autoCompleteParent);
            PostCodeService common = new PostCodeService(_postCodeDataAccess.Object);
            var details = common.AutocompletePostcode(postcode);
            Assert.AreEqual(details.Count, 10);
        }

        [TestMethod]
        public void AutocompletePostcodeCatch()
        {
            string postcode = "SW1W 0NY";
            AutoCompleteParent autoCompleteParent = new AutoCompleteParent()
            {
                Status = 200,
                Result = new Dictionary<string, string> { }
                
            };
            _postCodeDataAccess.Setup(x => x.AutocompletePostcode(It.IsAny<string>())).Returns(autoCompleteParent);
            PostCodeService common = new PostCodeService(_postCodeDataAccess.Object);
            var details = common.AutocompletePostcode(postcode);
            Assert.AreEqual(details.Count, 0);
        }
    }
}