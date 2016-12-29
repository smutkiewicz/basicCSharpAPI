using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PremierLeagueDashboardApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        public Table tableList;
        public AllFixtures allFixturesList;
        public Team team;
        public TeamPlayers teamPlayers;
        public static string ourTeamName;

        private async void Window_Initialized(object sender, EventArgs e)
        {
            ourTeamName = "Manchester United FC";
            allFixturesList = await NetTasks.GetAllFixtures();
            initFixturesPanel();
            team = await NetTasks.GetTeam();
            initTeam();
            teamPlayers = await NetTasks.GetPlayers();
            initPlayersList();
            tableList = await NetTasks.GetTable();
            initTable();
            progressBar.Visibility = Visibility.Hidden;
        }

        private void initTeam()
        {
            if(team != null)
            {
                squadValueTextBlock.Text = "Squad market value: " + team.squadMarketValue;
                mainTeamNameTextBlock.Text = team.name;
            }
        }

        private void initTable()
        {
            if (tableList.standing != null)
            {
                foreach (Club c in tableList.standing)
                {
                    ListViewItem item = new ListViewItem();
                    item.Content = c.toString();
                    table.Items.Add(item);
                }
            }
        }

        private void initPlayersList()
        {
            if(teamPlayers != null)
            {
                dataGrid.ItemsSource = teamPlayers.players;
            }
        }

        private void initFixturesPanel()
        {
            if (allFixturesList.fixtures != null)
            {
                Fixture p = allFixturesList.fixtures.ElementAt(0);

                Stack<string> stack = new Stack<string>();
                Stack<int> goalsStack = new Stack<int>();

                foreach (Fixture f in allFixturesList.fixtures)
                {
                    if (f.status == "SCHEDULED")
                    {
                        teamsTextBlock.Text = f.homeTeamName + " vs " + f.awayTeamName;
                        resultLastTextBlock.Text = "- : -";
                        matchdayTextBlock.Text = "Matchday: " + f.matchday;
                        timeTextBlock.Text = f.date.Replace('T', ' ');

                        teamsLastTextBlock.Text = p.homeTeamName + " vs " + p.awayTeamName;
                        resultLastTextBlock.Text = p.result.goalsHomeTeam + " : " + p.result.goalsAwayTeam;
                        matchdayLastTextBlock.Text = "Matchday: " + p.matchday;
                        timeLastTextBlock.Text = p.date.Replace('T', ' ');
                        break;
                    }
                    else
                    {
                        stack.Push(resultOfAMatch(f.result.goalsHomeTeam, f.result.goalsAwayTeam, f.homeTeamName));
                        p = f;
                    }
                }

                for (int i = 0; i < 6; i++)
                {
                    if (stack.Any()) formTextBlock.Text = stack.Pop() + " " + formTextBlock.Text;
                    else break;
                }
            }
        }

        private string resultOfAMatch(int? home, int? away, string homeTeam)
        {
            string win = "W";
            string lose = "L";
            string draw = "D";

            if(homeTeam == ourTeamName)
            {
                if (home > away) return win;
                else if (home == away) return draw;
                else return lose;
            }
            else
            {
                if (home > away) return lose;
                else if (home == away) return draw;
                else return win;
            }
        }
    }
}
