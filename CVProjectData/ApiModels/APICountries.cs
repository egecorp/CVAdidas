using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CVProject.Models;
using Newtonsoft.Json;

namespace CVProject.Data
{
    [JsonObject]
    public class ApiCountries 
    {
        [JsonObject]
        public class ApiCountriesApi
        {
            [JsonProperty]
            public int results { set; get; }

            [JsonProperty]
            public List<Country> countries { set; get; }

            [JsonProperty]
            public string error { set; get; }

        }

        public ApiCountriesApi api { set; get; }

        #region Static
        /// <summary>
        /// Объект для блокировки потока
        /// </summary>
        private static object mLock = new object();

        /// <summary>
        /// Страны каждый час не появляются - можно скачать всего один раз
        /// </summary>
        private static List<Country> CashedCountry = null;

        /// <summary>
        /// Возвращает все страны
        /// </summary>
        /// <param name="LastError">Пишет сюда ошибку при её наличии</param>
        /// <returns></returns>
        public static List<Country> GetCountries(ref string LastError)
        {
            try
            {
                lock (mLock)
                {
                    if (CashedCountry != null) return CashedCountry.ToList();

                    using (WebClient ws = new WebClient())
                    {
                        ws.Headers.Add("X-RapidAPI-Key", APIConfig.Get().Token);
                        string url = APIConfig.Get().GetCountry;
                        string body = ws.DownloadString(url);

                        if (body == null)
                        {
                            LastError = "Отсутствует ответ от API сервера";
                            return null;
                        }

                        ApiCountries countries = JsonConvert.DeserializeObject<ApiCountries>(body);
                        if ((countries == null) || (countries.api == null))
                        {
                            LastError = "Отсутствует ответ от API сервера";
                            return null;
                        }

                        if ((countries.api.error ?? "") != "")
                        {
                            LastError = countries.api.error;
                            return null;
                        }

                        if (countries.api.countries != null) CashedCountry = countries.api.countries;
                        return (CashedCountry == null) ? new List<Country>() : CashedCountry;
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
