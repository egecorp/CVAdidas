using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;

namespace CVProject.Models
{
    [JsonObject]
    public class APIConfig
    {
        public string ConnectionString { set; get; }

        public string GetCountry { set; get; }

        public string GetLeaguas { set; get; }

        public string GetTeams { set; get; }

        public string Token { set; get; }


        private const string DEFALUT_JSON_FILENAME = "apisettings.json";

        private static object mLock = new object();

        private static APIConfig myConfig = null;

        public static APIConfig Get(string jsonFileName = null)
        {
            lock (mLock)
            {
                if (myConfig != null) return myConfig;

                string settingsFileName = (jsonFileName != null) ? jsonFileName : null;

                if (settingsFileName == null)
                {
                    settingsFileName = Path.Combine(Directory.GetCurrentDirectory(), DEFALUT_JSON_FILENAME);
                }


                string fBody = File.ReadAllText(settingsFileName, System.Text.Encoding.UTF8);
                APIConfig newConfig = JsonConvert.DeserializeObject<APIConfig>(fBody);

                return myConfig = newConfig;
            }
        }
    }
}