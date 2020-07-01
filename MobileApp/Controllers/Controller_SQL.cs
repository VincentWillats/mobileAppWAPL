using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

/* SQL Controller 
 *  * 
 * Todo: 
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    28/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 01/07/2020 -- Added try/catches (whoops!)
 * 05/04/2020 -- Added LoadLeaderboardStats, LoadSeasonList
 * 30/03/2020 -- Updated to work with Actual Database structure of WAPL
 * 29/03/2020 -- Working LoadResultsAsync(), LoadPlayerSearchedListAsync()
 * 28/03/2020 -- File made, working LoadUpcomngTournamentsAsync();
 */

namespace MobileApp
{
    public class Controller_SQL
    {
        static string webAppURL = "https://waplmobile.azurewebsites.net/api/";
        static HttpClient client = new HttpClient();

        public static async Task<List<Data_Tournament>> LoadUpcomingTournamentsAsync()
        {
            string funcName = "GetUpcomingTournaments";
            List<Data_Tournament> upcomingTournamentList = new List<Data_Tournament>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(webAppURL + funcName);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                upcomingTournamentList = JsonConvert.DeserializeObject<List<Data_Tournament>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }   
            
            return upcomingTournamentList;
        }

        public static async Task<List<Data_Result>> LoadResultsAsync()
        {
            string funcName = "GetResultsGeneral";
            List<Data_Result>       results =              new List<Data_Result>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(webAppURL + funcName);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<Data_Result>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }

            return results;
        }

        public static async Task<List<Data_Player>> LoadPlayerSearchedListAsync(string searchStr)
        {
            string funcName = "GetPlayerSearchList";
            List<Data_Player> players = new List<Data_Player>();
           
            try
            {
                var stringToSearch = new StringContent(searchStr);
                HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, stringToSearch);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                players = JsonConvert.DeserializeObject<List<Data_Player>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }

            return players;
        }
        
        public static async Task<List<Data_ResultDetailPlayer>> LoadResultDetailsAsyn(int tournyID)
        {
            string funcName = "GetResultDetailPlayers";
            List<Data_ResultDetailPlayer> players = new List<Data_ResultDetailPlayer>();

            try
            {
                var strTournyID = new StringContent(tournyID.ToString());

                HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strTournyID);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                players = JsonConvert.DeserializeObject<List<Data_ResultDetailPlayer>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }


            return players;
        }

        public static async Task<List<Data_Image>> LoadResultImages(int tournyID)
        {
            string funcName = "GetResultImages";
            List<Data_Image> images = new List<Data_Image>();
            try
            {
                var strTournyID = new StringContent(tournyID.ToString());

                HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strTournyID);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                images = JsonConvert.DeserializeObject<List<Data_Image>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }

            return images;
        }

        public static async Task<Data_PlayerStats> LoadPlayerStats(int playerID, int seasonID)
        {
            Data_PlayerStats playerStats = new Data_PlayerStats();
            string funcName = "GetPlayerStats";
            try
            {
                var searchIDs = new StringContent(playerID.ToString() + "+" + seasonID.ToString());

                HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, searchIDs);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                playerStats = JsonConvert.DeserializeObject<Data_PlayerStats>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }

            return playerStats;
        }

        public static async Task<List<int>> LoadPlayerPlayedSeasons(int playerID)
        {
            List<int> seasonsPlayedIn = new List<int>();
            string funcName = "GetPlayerSeasonList";
            var strPlayerID = new StringContent(playerID.ToString());
            try
            {
                HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strPlayerID);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(responseBody);
                seasonsPlayedIn = JsonConvert.DeserializeObject<List<int>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }

            return seasonsPlayedIn;
        }

        public static async Task<List<int>> LoadSeasonList()
        {
            List<int> seasons = new List<int>();
            string funcName = "GetSeasonList";
            try
            {
                HttpResponseMessage response = await client.GetAsync(webAppURL + funcName);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(responseBody);
                seasons = JsonConvert.DeserializeObject<List<int>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }

            return seasons;
        }

        public static async Task<List<Data_LeaderboardEntry>> LoadLeaderboardStats(int seasonID)
        {
            string funcName = "GetLeaderboardEntries";
            List<Data_LeaderboardEntry> leaderboardEntries = new List<Data_LeaderboardEntry>();
            try
            {
                var strTournyID = new StringContent(seasonID.ToString());

                HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strTournyID);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                leaderboardEntries = JsonConvert.DeserializeObject<List<Data_LeaderboardEntry>>(responseBody);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine(ea.Message);
            }

            return leaderboardEntries;
        }
    }
}
