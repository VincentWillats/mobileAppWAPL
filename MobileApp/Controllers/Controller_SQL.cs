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
 * 
 * 
 * Todo:
 * Lots  
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    28/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 05/04/2020 -- Added LoadLeaderboardStats, LoadSeasonList
 * 30/03/2020 -- Updated to work with Actual Database structure of WAPL
 * 29/03/2020 -- Working LoadResultsAsync(), LoadPlayerSearchedListAsync()
 * 28/03/2020 -- File made, working LoadUpcomngTournamentsAsync();
 */

namespace MobileApp
{
    public class Controller_SQL
    {
        string webAppURL = "https://waplmobile.azurewebsites.net/api/";
        HttpClient client = new HttpClient();

        public async Task<List<Data_Tournament>> LoadUpcomingTournamentsAsync()
        {
            string funcName = "GetUpcomingTournaments";
            List<Data_Tournament> upcomingTournamentList = new List<Data_Tournament>();

            HttpResponseMessage response = await client.GetAsync(webAppURL + funcName);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            upcomingTournamentList = JsonConvert.DeserializeObject<List<Data_Tournament>>(responseBody);
            return upcomingTournamentList;
        }


        public async Task<List<Data_Result>> LoadResultsAsync()
        {
            string funcName = "GetResultsGeneral";
            List<Data_Result>       results =               new List<Data_Result>();

            HttpResponseMessage response = await client.GetAsync(webAppURL + funcName);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();  
            results = JsonConvert.DeserializeObject<List<Data_Result>>(responseBody);  
            return results;
        }

        public async Task<List<Data_Player>> LoadPlayerSearchedListAsync(string searchStr)
        {
            string funcName = "GetPlayerSearchList";
            List<Data_Player> players = new List<Data_Player>();

            var stringToSearch = new StringContent(searchStr);
            
            HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, stringToSearch);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();            
            players = JsonConvert.DeserializeObject<List<Data_Player>>(responseBody);

            return players;
        }


        public async Task<List<Data_ResultDetailPlayer>> LoadResultDetailsAsyn(int tournyID)
        {
            string funcName = "GetResultDetailPlayers";
            List<Data_ResultDetailPlayer> players = new List<Data_ResultDetailPlayer>();

            var strTournyID = new StringContent(tournyID.ToString());

            HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strTournyID);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            players = JsonConvert.DeserializeObject<List<Data_ResultDetailPlayer>>(responseBody);

            return players;
        }

        public async Task<List<Data_Image>> LoadResultImages(int tournyID)
        {
            string funcName = "GetResultImages";
            List<Data_Image> images = new List<Data_Image>();
            
            var strTournyID = new StringContent(tournyID.ToString());
            
            HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strTournyID);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            images = JsonConvert.DeserializeObject<List<Data_Image>>(responseBody);
            System.Diagnostics.Debug.WriteLine("Image count: " + images.Count);
            return images;
        }

        public async Task<Data_PlayerStats> LoadPlayerStats(int playerID, int seasonID)
        {
            Data_PlayerStats playerStats = new Data_PlayerStats();
            string funcName = "GetPlayerStats";
            var searchIDs = new StringContent(playerID.ToString() + "+" + seasonID.ToString());

            HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, searchIDs);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            playerStats = JsonConvert.DeserializeObject<Data_PlayerStats>(responseBody);
            return playerStats;
        }

        public async Task<List<int>> LoadPlayerPlayedSeasons(int playerID)
        {
            List<int> seasonsPlayedIn = new List<int>();
            string funcName = "GetPlayerSeasonList";
            var strPlayerID = new StringContent(playerID.ToString());

            HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strPlayerID);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(responseBody);
            seasonsPlayedIn = JsonConvert.DeserializeObject<List<int>>(responseBody);
            return seasonsPlayedIn;
        }

        public async Task<List<int>> LoadSeasonList()
        {
            List<int> seasons = new List<int>();
            string funcName = "GetSeasonList";
            HttpResponseMessage response = await client.GetAsync(webAppURL + funcName);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(responseBody);
            seasons = JsonConvert.DeserializeObject<List<int>>(responseBody);
            return seasons;
        }


        public async Task<List<Data_LeaderboardEntry>> LoadLeaderboardStats(int seasonID)
        {
            string funcName = "GetLeaderboardEntries";
            List<Data_LeaderboardEntry> leaderboardEntries = new List<Data_LeaderboardEntry>();

            var strTournyID = new StringContent(seasonID.ToString());

            HttpResponseMessage response = await client.PostAsync(webAppURL + funcName, strTournyID);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            leaderboardEntries = JsonConvert.DeserializeObject<List<Data_LeaderboardEntry>>(responseBody);
            return leaderboardEntries;
        }
    }
}
