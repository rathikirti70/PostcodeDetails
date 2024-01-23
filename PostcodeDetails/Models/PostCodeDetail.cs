using Newtonsoft.Json;
using PostcodeDetails.Controllers;
using System.Runtime.Serialization;

namespace PostcodeDetails.Models
{
    public class PostCodeDetail
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]

        public string Region { get; set; }

        [JsonProperty("admin_district")]

        public string AdminDistrict { get; set; }

        //[DataMember(Name = "parliamentary_constituency")]
        [JsonProperty("parliamentary_constituency")]

        public string ParliamentaryConstituency { get; set; }
        
        public string Area
        {
            get
            {
                string area = string.Empty;
                if (Latitude < 52.229466)
                {
                    area = "South";
                }
                else if (Latitude >= 52.229466 && Latitude <= 53.27169)
                {
                    area = "Midlands";
                }
                else
                {
                    area = "North";
                }
                return area;
            }
        }
        [JsonProperty("latitude")]
        [JsonIgnore]

        public float Latitude { get;  set; }

    }

    public class PostCodeDetailParent
    {
        [JsonProperty("result")]
        public PostCodeDetail Result { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }


    public class AutoCompleteList
    {
        [JsonProperty("result")]
        public List<string> Result { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        
    }

    public class AutoCompleteParent
    {
        [JsonProperty("result")]
        public Dictionary<string,string> Result { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

    }

}
