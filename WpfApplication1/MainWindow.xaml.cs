using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            try
            {
                allFixturesList = await NetTasks.GetAllFixtures();
                initFixturesPanel();
                team = await NetTasks.GetTeam();
                initTeam();
                teamPlayers = await NetTasks.GetPlayers();
                initPlayersList();
                tableList = await NetTasks.GetTable();
                initTable();
            }
            catch
            {
                MessageBox.Show("You've probably reached your request limit or have problems with connection. Try later.", 
                    "Connection failed");
            }

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
                table.ItemsSource = tableList.standing;
            }
        }

        private void initPlayersList()
        {
            if(teamPlayers != null)
            {
                dataGrid.ItemsSource = teamPlayers.players;
                dataGrid.IsReadOnly = true;
            }
        }

        private void initFixturesPanel()
        {
            if (allFixturesList.fixtures != null)
            {
                Fixture p = allFixturesList.fixtures.ElementAt(0); // last match

                Stack<string> stack = new Stack<string>();
                Stack<int?> goalsPlusStack = new Stack<int?>();
                Stack<int?> goalsMinusStack = new Stack<int?>();

                foreach (Fixture f in allFixturesList.fixtures)
                {
                    if (f.status == "FINISHED" || f.status == "POSTPONED")
                    {
                        calculateGoals(f, goalsPlusStack, goalsMinusStack);
                        stack.Push(resultOfAMatch(f.result.goalsHomeTeam, f.result.goalsAwayTeam, f.homeTeamName));
                        p = f;
                    }
                    else //if(f.status == "SCHEDULED" || f.status == "TIMED" || f.status == "IN_PLAY")
                    {
                        if (f.status == "IN_PLAY")
                        {
                            nextFixtureGroupBox.Header = "Live matchday";

                            if (f.result.goalsHomeTeam != null && f.result.goalsAwayTeam != null)
                            {
                                resultTextBlock.Text = f.result.goalsHomeTeam + " : " + f.result.goalsAwayTeam;
                            }
                            else
                            {
                                resultTextBlock.Text = "0 : 0";
                            }
                        }

                        nextFixtureGroupBox.Header = "Next fixture";
                        resultTextBlock.Text = "- : -";

                        // scheduled match
                        teamsTextBlock.Text = f.homeTeamName + " vs " + f.awayTeamName;
                        matchdayTextBlock.Text = "Matchday: " + f.matchday;
                        timeTextBlock.Text = f.date.Replace('T', ' ');
                        if (ourTeamName == f.homeTeamName) venueTextBlock.Text = "HOME";
                        else venueTextBlock.Text = "AWAY";

                        // last match
                        teamsLastTextBlock.Text = p.homeTeamName + " vs " + p.awayTeamName;
                        resultLastTextBlock.Text = p.result.goalsHomeTeam + " : " + p.result.goalsAwayTeam;
                        matchdayLastTextBlock.Text = "Matchday: " + p.matchday;
                        timeLastTextBlock.Text = p.date.Replace('T', ' ');
                        if (ourTeamName == p.homeTeamName) venueLastTextBlock.Text = "HOME";
                        else venueLastTextBlock.Text = "AWAY";
                        break;
                    }
                }

                initFormPanel(stack, goalsPlusStack, goalsMinusStack);
            }
        }

        private void initFormPanel(Stack<string> stack, Stack<int?> goalsPlusStack, Stack<int?> goalsMinusStack)
        {
            int? plus = 0;
            int? minus = 0;
            formTextBlock.Text = "";

            for (int i = 0; i < 5; i++)
            {
                if (stack.Any()) formTextBlock.Text = stack.Pop() + " " + formTextBlock.Text;
                else break;

                if (goalsPlusStack.Any() && goalsMinusStack.Any())
                {
                    plus += goalsPlusStack.Pop();
                    minus += goalsMinusStack.Pop();
                }
            }

            goalsTextBlock.Text = "G+" + plus + " G-" + minus;
        }

        private void calculateGoals(Fixture f, Stack<int?> goalsPlusStack, Stack<int?> goalsMinusStack)
        {
            if(ourTeamName == f.homeTeamName)
            {
                goalsMinusStack.Push(f.result.goalsAwayTeam);
                goalsPlusStack.Push(f.result.goalsHomeTeam);
            }
            else if(ourTeamName == f.awayTeamName)
            {
                goalsMinusStack.Push(f.result.goalsHomeTeam);
                goalsPlusStack.Push(f.result.goalsAwayTeam);
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

        private void progressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window_Initialized(button, e);
        }
    }
}
