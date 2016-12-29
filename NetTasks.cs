using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PremierLeagueDashboardApp
{
    public static class NetTasks
    {
        private static string adress = "http://api.football-data.org/v1/competitions/426/leagueTable";
        private static string fixtures = "http://api.football-data.org/v1/teams/66/fixtures?timeFrame=n14";
        private static string allFixtures = "http://api.football-data.org/v1/teams/66/fixtures";
        private static string team = "http://api.football-data.org/v1/teams/66";
        private static string players = "http://api.football-data.org/v1/teams/66/players/";
        private static string token = "-";
        public async static Task<Table> GetTable()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("method", "GET");
            client.DefaultRequestHeaders.Add("header", "X-Auth-Token: "+token);
            
            HttpResponseMessage response = await client.GetAsync(adress);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();

            if (result != null)
            {
                return Parser.ParseTable(result);
            }
            else
            {
                return null;
            }
        }

        public async static Task<AllFixtures> GetRecentFixtures()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("method", "GET");
            client.DefaultRequestHeaders.Add("header", "X-Auth-Token: " + token);

            HttpResponseMessage response = await client.GetAsync(fixtures);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();


            if (result != null)
            {
                return Parser.ParseRecentFixtures(result);
            }
            else
            {
                return null;
            }
        }

        public async static Task<Team> GetTeam()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("method", "GET");
            client.DefaultRequestHeaders.Add("header", "X-Auth-Token: " + token);
            HttpResponseMessage response = await client.GetAsync(team);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();

            if (result != null)
            {
                return Parser.ParseTeam(result);
            }
            else
            {
                return null;
            }
        }

        public async static Task<TeamPlayers> GetPlayers()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("method", "GET");
            client.DefaultRequestHeaders.Add("header", "X-Auth-Token: " + token);

            HttpResponseMessage response = await client.GetAsync(players);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();

            if (result != null)
            {
                return Parser.ParsePlayers(result);
            }
            else
            {
                return null;
            }
        }

        public async static Task<AllFixtures> GetAllFixtures()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("method", "GET");
            client.DefaultRequestHeaders.Add("header", "X-Auth-Token: " + token);

            HttpResponseMessage response = await client.GetAsync(allFixtures);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();

            if(result != null)
            {
                return Parser.ParseAllFixtures(result);
            }
            else
            {
                return null;
            }
        }

    }
}
