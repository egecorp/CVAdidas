using System.Collections.Generic;
using CVProject.Models;


namespace CVProject.Data
{
    /// <summary>
    /// Источник данных для отображения
    /// </summary>
    public static class DataSource
    {

        #region Team
        /// <summary>
        /// Вернуть все команды
        /// </summary>
        /// <param name="LeaguaId">Идентификатор лиги</param>
        public static List<Team> GetTeams(string LeagueId, ref string requestError)
        {
            requestError = null;
            List<Team> answer = ApiTeams.GetTeams(LeagueId, ref requestError);
            if (answer == null) return new List<Team>();
            return answer;
        }

        #endregion

        #region Country
        /// <summary>
        /// Вернуть все страны
        /// </summary>
        public static List<Country> GetCountries(ref string requestError)
        {
            requestError = null;
            List<Country> answer = ApiCountries.GetCountries(ref requestError);
            if (answer == null) return new List<Country>();
            return answer;
        }

        #endregion


        #region Leagues
        /// <summary>
        /// Вернуть все лиги в стране
        /// </summary>
        /// <param name="CountryCode">Идентификатор страны</param>
        public static List<League> GetLeagues(string CountryCode, ref string requestError)
        {
            requestError = null;
            List<League> answer = ApiLeagues.GetLeague(CountryCode, ref requestError);
            if (answer == null) return new List<League>();
            return answer;
        }

        #endregion


    }
}
