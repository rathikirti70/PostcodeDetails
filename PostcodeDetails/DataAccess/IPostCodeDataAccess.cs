using PostcodeDetails.Models;

namespace PostcodeDetails.DataAccess
{
    public interface IPostCodeDataAccess
    {
        PostCodeDetailParent LookupPostcode(string postcode);
        AutoCompleteParent AutocompletePostcode(string postcode);
    }
}
