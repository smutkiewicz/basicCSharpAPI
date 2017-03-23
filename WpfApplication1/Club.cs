using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremierLeagueDashboardApp
{
    public class Club
    {
        public int position;
        public string teamName;
        public int teamId { get; set; }
        public int playedGames;
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

        public string TeamName
        {
            get
            {
                return teamName;
            }

            set
            {
                teamName = value;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int PlayedGames
        {
            get
            {
                return playedGames;
            }

            set
            {
                playedGames = value;
            }
        }

        public int Points
        {
            get
            {
                return points;
            }

            set
            {
                points = value;
            }
        }

        public int GoalDifference
        {
            get
            {
                return goalDifference;
            }

            set
            {
                goalDifference = value;
            }
        }
    }
}
