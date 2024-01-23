using Newtonsoft.Json;
using PostcodeDetails.Models;
using System.Net.Http.Headers;
using System.Net;
using PostcodeDetails.Common;
using System.Reflection.Metadata.Ecma335;

namespace PostcodeDetails.DataAccess
{
    public class PostCodeDataAccess : IPostCodeDataAccess
    {
        public PostCodeDetailParent LookupPostcode(string postcode)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Constants.BASE_URL);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetDepartments using HttpClient  
                HttpResponseMessage Res = client.GetAsync(postcode).Result;
                //Checking the response is successful or not which is sent using HttpClient  

                return JsonConvert.DeserializeObject<PostCodeDetailParent>(Res.Content.ReadAsStringAsync().Result);
            }
        }

        public AutoCompleteParent AutocompletePostcode(string partialPostcode)
        {
            AutoCompleteParent autoCompleteParent = new AutoCompleteParent()
            {
                Result = new Dictionary<string, string>()
            };
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Constants.BASE_URL);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetDepartments using HttpClient  
                HttpResponseMessage Res = client.GetAsync(partialPostcode + "/autocomplete").Result;

                //Checking the response is successful or not which is sent using HttpClient  
                var ObjResponse = JsonConvert.DeserializeObject<AutoCompleteList>(Res.Content.ReadAsStringAsync().Result);


                autoCompleteParent.Status = ObjResponse.Status;
                if (ObjResponse.Status == (int)HttpStatusCode.OK)
                {
                    if (ObjResponse.Result != null)
                    {
                        foreach (var post in ObjResponse.Result)
                        {
                            autoCompleteParent.Result.Add(post, LookupPostcode(post.ToString()).Result.Area);
                        }
                    }
                }            
               
            }
            return autoCompleteParent;
        }


    }
}
