using CVProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CVProject.Data
{
    [JsonObject]
    public class ApiLeagues
    {
        [JsonObject]
        public class ApiLeaguesApi
        {
            [JsonProperty]
            public int results { set; get; }

            [JsonProperty]
            public List<League> leagues { set; get; }

            [JsonProperty]
            public string error { set; get; }
        }

        public ApiLeaguesApi api { set; get; }



        #region Static
        /// <summary>
        /// Объект для блокировки потока
        /// </summary>
        private static object mLock = new object();

        /// <summary>
        /// Турниры каждый час не появляются - можно скачать всего один раз для каждой страны
        /// </summary>
        private static Dictionary<string, List<League>> CashedLeagues = null;

        public static List<League> GetLeague(string CountryCode, ref string LastError)
        {
            try
            {
                lock(mLock)
                {
                    if (CashedLeagues == null) CashedLeagues = new Dictionary<string, List<League>>();

                    if (CashedLeagues.ContainsKey(CountryCode)) return CashedLeagues[CountryCode].ToList();
                    using (WebClient ws = new WebClient())
                    {
                        ws.Headers.Add("X-RapidAPI-Key", APIConfig.Get().Token);
                        string url = APIConfig.Get().GetLeaguas + "/" + CountryCode;
                        string body = ws.DownloadString(url);

                        if (body == null)
                        {
                            LastError = "Отсутствует ответ от API сервера";
                            return null;
                        }

                        ApiLeagues leagues = JsonConvert.DeserializeObject<ApiLeagues>(body);
                        if ((leagues == null) || (leagues.api == null))
                        {
                            LastError = "Отсутствует ответ от API сервера";
                            return null;
                        }

                        if ((leagues.api.error ?? "") != "")
                        {
                            LastError = leagues.api.error;
                            return null;
                        }

                        if (leagues.api.leagues != null)
                        {
                            CashedLeagues.Add(CountryCode, leagues.api.leagues);
                            return leagues.api.leagues;
                        }
                        CashedLeagues.Add(CountryCode, new List<League>());

                        return new List<League>();
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


