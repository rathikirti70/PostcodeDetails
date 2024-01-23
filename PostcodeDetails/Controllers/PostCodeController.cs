using Microsoft.AspNetCore.Mvc;
using PostcodeDetails.Services;

namespace PostcodeDetails.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PostcodeController : ControllerBase
    {
        private readonly ILogger<PostcodeController> _logger;
        private readonly IPostCodeService _commonRepo;
        
        public PostcodeController(ILogger<PostcodeController> logger, IPostCodeService commonRepo)
        {
            _logger = logger;
            _commonRepo = commonRepo;

        }
        [HttpGet]
        public IActionResult Autocomplete(string partialPostcode)
        {
            try
            {
                if(string.IsNullOrEmpty(partialPostcode))
                {
                    return BadRequest("Please enter partial post code");
                }
                var test = _commonRepo.AutocompletePostcode(partialPostcode);
                return Ok(test);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        public IActionResult Lookup(string postcode)
        {
            try
            {
                if (string.IsNullOrEmpty(postcode))
                {
                    return BadRequest("Please enter post code");
                }
                var test = _commonRepo.LookupPostcode(postcode);
                return Ok(test);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
