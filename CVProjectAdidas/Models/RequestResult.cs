using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CVProject.Models
{
    /// <summary>
    /// Универсальный класс для ответов сервера на запросы из фронта
    /// </summary>
    [JsonObject]
    public class RequestResult
    {
        [JsonProperty]
        public string Error { set; get; }

        [JsonProperty]
        public List<League> LeagueList { set; get; }

        [JsonProperty]
        public List<Team> TeamList { set; get; }

        [JsonProperty]
        public Team OneTeam { set; get; }


        public static string GetErrorAnswer(string txt)
        {
            RequestResult answer = new RequestResult()
            {
                Error = txt
            };

            return JsonConvert.SerializeObject(answer);
        }


    }
}
