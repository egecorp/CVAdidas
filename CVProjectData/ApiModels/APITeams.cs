using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CVProject.Models;
using Newtonsoft.Json;


namespace CVProject.Data
{
    [JsonObject]
    public class ApiTeams
    {
        [JsonObject]
        public class ApiTeamsApi
        {
            [JsonProperty]
            public int results { set; get; }

            [JsonProperty]
            public List<Team> teams { set; get; }

            [JsonProperty]
            public string error { set; get; }
        }

        public ApiTeamsApi api { set; get; }


        #region Static
        /// <summary>
        /// Объект для блокировки потока
        /// </summary>
        private static object mLock = new object();

        /// <summary>
        /// Команды каждый час не появляются - можно скачать всего один раз для каждого турнира
        /// </summary>
        private static Dictionary<string, List<Team>> CashedTeams = null;

        /// <summary>
        /// Вернуть команды по идентификатору турнира
        /// </summary>
        /// <param name="LeagueId">идентификатор турнира</param>
        /// <param name="LastError">поле для записи ошибки</param>
        /// <returns></returns>
        public static List<Team> GetTeams(string LeagueId, ref string LastError)
        {
            try
            {
                lock (mLock)
                {
                    if (CashedTeams == null) CashedTeams = new Dictionary<string, List<Team>>();

                    if (CashedTeams.ContainsKey(LeagueId)) return CashedTeams[LeagueId].ToList();

                    using (WebClient ws = new WebClient())
                    {
                        ws.Headers.Add("X-RapidAPI-Key", APIConfig.Get().Token);
                        string url = APIConfig.Get().GetTeams + "/" + LeagueId;
                        string body = ws.DownloadString(url);

                        if (body == null)
                        {
                            LastError = "Отсутствует ответ от API сервера";
                            return null;
                        }

                        ApiTeams teams = JsonConvert.DeserializeObject<ApiTeams>(body);
                        if ((teams == null) || (teams.api == null))
                        {
                            LastError = "Отсутствует ответ от API сервера";
                            return null;
                        }

                        if ((teams.api.error ?? "") != "")
                        {
                            LastError = teams.api.error;
                            return null;
                        }

                        if (teams.api.teams != null)
                        {
                            CashedTeams.Add(LeagueId, teams.api.teams);
                            return teams.api.teams;
                        }
                        CashedTeams.Add(LeagueId, new List<Team>());

                        return new List<Team>();

                    }

                }
            }
            catch (Exception e)
            {
                LastError = e.Message;
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion
    }
}


