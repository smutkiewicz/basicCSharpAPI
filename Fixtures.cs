using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremierLeagueDashboardApp
{
    public class Fixture
    {
        public int id { get; set; }
        public string competition { get; set; }
        public int competitionId { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public int matchday { get; set; }
        public string homeTeamName { get; set; }
        public int homeTeamId { get; set; }
        public string awayTeamName { get; set; }
        public int awayTeamId { get; set; }
        public MatchResult result { get; set; }
    }
}
