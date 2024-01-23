using PostcodeDetails.Models;

namespace PostcodeDetails.Services
{
    public interface IPostCodeService
    {
        public Dictionary<string, string> AutocompletePostcode(string partialPostcode);

        public PostCodeDetail LookupPostcode(string postcode);
    }
}
