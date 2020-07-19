using Newtonsoft.Json;

namespace CVProject.Models
{
    [JsonObject]
    public class Country
    {
        [JsonProperty]
        public string country { set; get; }

        [JsonProperty]
        public string code { set; get; }

        [JsonProperty]
        public string flag { set; get; }

    }
}
