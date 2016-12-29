using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremierLeagueDashboardApp
{
    public class Club
    {
        public int position { get; set; }
        public string teamName { get; set; }
        public int teamId { get; set; }
        public int playedGames { get; set; }
        public string crestURI { get; set; }
        public int points { get; set; }
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }

        public Club()
        {

        }

        public string toString()
        {
            return position + ". " + teamName + " - " + points +" pts";
        }

        public string CrestURI
        {
            get
            {
                return crestURI;
            }

            set
            {
                crestURI = value;
            }
        }
    }
}
