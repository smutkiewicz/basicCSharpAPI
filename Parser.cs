using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PremierLeagueDashboardApp
{
    public class Parser
    {
        public static Table ParseTable(string content)
        {
            Table table = JsonConvert.DeserializeObject<Table>(content);
            return table;
        }

        public static AllFixtures ParseRecentFixtures(string content)
        {
            AllFixtures result = JsonConvert.DeserializeObject<AllFixtures>(content);
            return result;
        }

        public static Team ParseTeam(string content)
        {
            Team result = JsonConvert.DeserializeObject<Team>(content);
            return result;
        }

        public static TeamPlayers ParsePlayers(string content)
        {
            TeamPlayers result = JsonConvert.DeserializeObject<TeamPlayers>(content);
            return result;
        }

        public static AllFixtures ParseAllFixtures(string content)
        {
            AllFixtures result = JsonConvert.DeserializeObject<AllFixtures>(content);
            return result;
        }
    }
}
