using Newtonsoft.Json;
using PostcodeDetails.Common;
using PostcodeDetails.DataAccess;
using PostcodeDetails.Models;
using System.Net;
using System.Net.Http.Headers;

namespace PostcodeDetails.Services
{
    public class PostCodeService : IPostCodeService
    {
        private readonly IPostCodeDataAccess postCodeData;

        public PostCodeService(IPostCodeDataAccess postCodeData)
        {
            this.postCodeData = postCodeData;
        }

        //public Dictionary<string, string> AutocompletePostcode(string partialPostcode)
        //{
        //    Dictionary<string, string> areas = new Dictionary<string, string>();
        //    using (var client = new HttpClient())
        //    {
        //        //Passing service base url  
        //        client.BaseAddress = new Uri(Constants.BASE_URL);

        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        //Sending request to find web api REST service resource GetDepartments using HttpClient  
        //        HttpResponseMessage Res = client.GetAsync(partialPostcode + "/autocomplete").Result;

        //        //Checking the response is successful or not which is sent using HttpClient  
        //        var ObjResponse = JsonConvert.DeserializeObject<AutoCompleteParent>(Res.Content.ReadAsStringAsync().Result);

        //        if (ObjResponse.Status == (int)HttpStatusCode.OK)
        //        {
        //            Parallel.ForEach(ObjResponse.Result, x =>
        //            {
        //                areas.Add(x.ToString(), LookupPostcode(x.ToString()).Area);
        //            });
        //        }
        //    }
        //    return areas;
        //}

        public Dictionary<string, string> AutocompletePostcode(string partialPostcode)
        {
            var ObjResponse = postCodeData.AutocompletePostcode(partialPostcode);
               return ObjResponse.Result;
            
            
            
        }

        public PostCodeDetail LookupPostcode(string postcode)
        {
            var ObjResponse = postCodeData.LookupPostcode(postcode);
            if (ObjResponse.Status == (int)HttpStatusCode.OK)
            {
                return ObjResponse.Result;
            }
            else
            {
                throw new ApplicationException(ObjResponse.Error);
            }
        }
    }
}
