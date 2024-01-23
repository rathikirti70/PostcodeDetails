using Microsoft.Extensions.Logging;
using PostcodeDetails.Controllers;
using PostcodeDetails.Models;
using PostcodeDetails.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostCodeDetailsTest
{
    [TestClass]
    public class PostCodeControllerTest
    {

        private readonly ILogger<PostcodeController> _logger;
        private readonly IPostCodeService _commonRepo;

        public PostCodeControllerTest(ILogger<PostcodeController> logger, IPostCodeService commonRepo)
        {
            _logger = logger;
            _commonRepo = commonRepo;

        }
        [TestMethod]
        public void LookupPostcodeTest()
        {
            var common = new PostcodeController(_logger, _commonRepo).Lookup("SW1W 0NY");
            Assert.IsNotNull(common);

        }

        [TestMethod]
        public void LookupPostcodeCatch()
        {
            try
            {
                var common = new PostcodeController(_logger, _commonRepo).Lookup("SW1");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ApplicationException);
            }

        }

        [TestMethod]
        public void AutoCompletePostcodeTest()
        {
            var common = new PostcodeController(_logger, _commonRepo).Autocomplete("SW1W");
            Assert.IsNotNull(common);

        }

        [TestMethod]
        public void AutoCompletePostcodeCatch()
        {
            try
            {
                var common = new PostcodeController(_logger, _commonRepo).Autocomplete("11");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ApplicationException);
            }

        }

    }
}
