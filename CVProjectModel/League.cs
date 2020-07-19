using Newtonsoft.Json;

namespace CVProject.Models
{
    [JsonObject]
    public class League
    {
        [JsonProperty]
        public string league_id { set; get; }

        [JsonProperty]
        public string name { set; get; }

        [JsonProperty]
        public string country_code{ set; get; }

        [JsonProperty]
        public int? season { set; get; }

        // Остальные данные в условиях текущей задачи рассматривать нет смысла

    }
}
