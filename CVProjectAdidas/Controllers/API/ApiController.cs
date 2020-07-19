using CVProject.Data;
using CVProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CVProject.Controllers
{
    public class ApiController : Controller
    {
        public ApiController()
        {

        }

        [HttpGet]
        public string GetLeagues([FromQuery]string CountryCode)
        {
            string requestError = null;
            List<League> leagueList = DataSource.GetLeagues(CountryCode, ref requestError);
            if (requestError != null) return RequestResult.GetErrorAnswer(requestError);

            RequestResult answer = new RequestResult()
            {
                LeagueList = leagueList.ToList()
            };

            return JsonConvert.SerializeObject(answer);
        }

        [HttpGet]
        public string GetTeams([FromQuery] string LeagueId)
        {
            string requestError = null;

            List<Team> teamList = DataSource.GetTeams(LeagueId, ref requestError);
            if (requestError != null) return RequestResult.GetErrorAnswer(requestError);

            RequestResult answer = new RequestResult()
            {
                TeamList = teamList.ToList()
            };

            return JsonConvert.SerializeObject(answer);
        }

    }
}
