using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace CVProject.Models
{
    [JsonObject]
    public class Team
    {

        [JsonProperty]
        public string name { set; get; }
        [JsonProperty]
        public string code { set; get; }
        [JsonProperty]
        public string logo { set; get; }

        [JsonProperty]
        public string country { set; get; }
        [JsonProperty]
        public string venue_name { set; get; }
        [JsonProperty]
        public string venue_surface { set; get; }
        [JsonProperty]
        public string venue_address { set; get; }
        [JsonProperty]
        public string venue_city { set; get; }


        [JsonProperty]
        public string is_national { set; get; }

        [JsonProperty]
        public int? team_id { set; get; }
        [JsonProperty]
        public int? venue_capacity { set; get; }
        [JsonProperty]
        public int? founded { set; get; }

        public Team()
        {

        }

    }

}
