using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BasketballGameApp.Services;
using BasketballGameApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using BasketballGameApp.Views;

namespace BasketballGameApp.ViewModels
{
    class GamesScoresViewModel : BaseViewModel
    {
        private App TheApp = (App)Application.Current;

        #region IsLoggedIn
        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set
            {
                isLoggedIn = value;
                OnPropertyChanged("IsLoggedIn");
            }
        }
        #endregion

        #region IsNotLoggedin
        private bool isNotLoggedin;
        public bool IsNotLoggedIn
        {
            get => isNotLoggedin;
            set
            {
                isNotLoggedin = value;
                OnPropertyChanged("IsNotLoggedin");
            }
        }
        #endregion

        #region IsCoachWithoutTeam
        private bool isCoachWithoutTeam;
        public bool IsCoachWithoutTeam
        {
            get => isCoachWithoutTeam;
            set
            {
                isCoachWithoutTeam = value;
                OnPropertyChanged("IsCoachWithoutTeam");
            }
        }
        #endregion

        #region IsPlayerWithoutRequest
        private bool isPlayerWithoutRequest;
        public bool IsPlayerWithoutRequest
        {
            get => isPlayerWithoutRequest;
            set
            {
                isPlayerWithoutRequest = value;
                OnPropertyChanged("IsPlayerWithoutRequest");
            }
        }
        #endregion

        #region IsCoachWithTeam
        private bool isCoachWithTeam;
        public bool IsCoachWithTeam
        {
            get => isCoachWithTeam;
            set
            {
                isCoachWithTeam = value;
                OnPropertyChanged("IsCoachWithTeam");
            }
        }
        #endregion

        #region IsPlayerWithRequest
        private bool isPlayerWithRequest;
        public bool IsPlayerWithRequest
        {
            get => isPlayerWithRequest;
            set
            {
                isPlayerWithRequest = value;
                OnPropertyChanged("IsPlayerWithRequest");
            }
        }
        #endregion

        #region HaveMinPlayers
        private bool haveMinPlayers;
        public bool HaveMinPlayers
        {
            get => haveMinPlayers;
            set
            {
                haveMinPlayers = value;
                OnPropertyChanged("HaveMinPlayers");
            }
        }
        #endregion

        #region IsUserWithTeam
        private bool isUserWithTeam;
        public bool IsUserWithTeam
        {
            get => isUserWithTeam;
            set
            {
                isUserWithTeam = value;
                OnPropertyChanged("IsUserWithTeam");
            }
        }
        #endregion

        #region ObservableCollectionGames
        private List<Game> listGames;
        private ObservableCollection<Game> observableCollectionGames;
        public ObservableCollection<Game> ObservableCollectionGames
        {
            get
            {
                return this.observableCollectionGames;
            }
            set
            {
                if (this.observableCollectionGames != value)
                {
                    this.observableCollectionGames = value;
                    OnPropertyChanged("ObservableCollectionGames");
                }
            }
        }
        #endregion

        #region Constructor
        public GamesScoresViewModel()
        {
            NavigateToPageCommand = new Command<string>(NavigateToPage);
            NavigateToLogOutPageCommand = new Command(NavigateToLogOutPage);
            NavigateToCreateTeamPageCommand = new Command(NavigateToCreateTeamPage);
            NavigateToJoinToGroupCommand = new Command(NavigateToJoinToGroupPage);
            NavigateToApproveRequestsToJoinTeamCommand = new Command(NavigateToApproveRequestsToJoinTeamPage);
            NavigateToRequestToSetGameCommand = new Command(NavigateToRequestToSetGamePage);
            NavigateToApproveRequestsToSetGameCommand = new Command(NavigateToApproveRequestsToSetGamePage);
            NavigateToViewRequestToJoinTeamCommand = new Command(NavigateToViewRequestToJoinTeamPage);
            NavigateToViewFutureGamesCommand = new Command(NavigateToViewFutureGamesPage);
            NavigateToSelectGameToEnterScoreCommand = new Command(NavigateToSelectGameToEnterScorePage);
            NavigateToViewTeamMembersCommand = new Command(NavigateToViewTeamMembersPage);
            NavigateToPlayersRankingCommand = new Command(NavigateToPlayersRankingPage);
            NavigateToTeamsRankingCommand = new Command(NavigateToTeamsRankingPage);
            observableCollectionGames = new ObservableCollection<Game>();
            
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            InitButtons();
            // אם המשתמש לא מחובר
           
            //add server status page
            //LoadGames();
        }
        #endregion
        public void InitButtons()
        {
            if (TheApp.CurrentUser == null)
            {
                IsLoggedIn = false;
                IsNotLoggedIn = true;
            }
            else // אם המשתמש מחובר
            {
                IsLoggedIn = true;
                IsNotLoggedIn = false;

                // האם למשתמש יש קבוצה
                if ((TheApp.CurrentCoach != null && TheApp.CurrentCoach.Team != null) || (TheApp.CurrentPlayer != null && TheApp.CurrentPlayer.Team != null))
                    IsUserWithTeam = true;
                else
                    IsUserWithTeam = false;

                // האם המשתמש הוא מאמן ללא קבוצה
                if (TheApp.CurrentCoach != null && TheApp.CurrentCoach.Team == null)
                {
                    IsCoachWithoutTeam = true;
                    IsPlayerWithoutRequest = false;
                    IsCoachWithTeam = false;
                    IsPlayerWithRequest = false;
                    HaveMinPlayers = false;
                }

                // האם המשתמש הוא שחקן שלא הגיש בקשה להצטרפות לקבוצה
                else if (TheApp.CurrentPlayer != null && TheApp.CurrentPlayer.RequestToJoinTeams.Count == 0 && TheApp.CurrentPlayer.Team == null)
                {
                    IsPlayerWithoutRequest = true;
                    IsCoachWithoutTeam = false;
                    IsCoachWithTeam = false;
                    IsPlayerWithRequest = false;
                }

                // האם המשתמש הוא מאמן עם קבוצה
                else if (TheApp.CurrentCoach != null && TheApp.CurrentCoach.Team != null)//לבדוק האם הקבוצה כבר מלאה
                {
                    IsCoachWithoutTeam = false;
                    IsPlayerWithoutRequest = false;
                    IsCoachWithTeam = true;
                    IsPlayerWithRequest = false;
                    // האם בקבוצה יש מספר שחקנים מינימלי
                    if (TheApp.CurrentCoach.Team.Players.Count() >= 0)//לשנות
                        HaveMinPlayers = true;
                    else
                        HaveMinPlayers = false;
                }

                // האם המשתמש הוא שחקן שהגיש בקשת הצטרפות לקבוצה
                else if (TheApp.CurrentPlayer != null && TheApp.CurrentPlayer.RequestToJoinTeams.Count != 0 && TheApp.CurrentPlayer.Team == null)
                {
                    IsPlayerWithoutRequest = false;
                    IsCoachWithoutTeam = false;
                    IsCoachWithTeam = false;
                    IsPlayerWithRequest = true;
                    HaveMinPlayers = false;
                }

                // אם לא מתקיים שום תנאי
                else
                {
                    IsCoachWithoutTeam = false;
                    IsPlayerWithoutRequest = false;
                    IsCoachWithTeam = false;
                    IsPlayerWithRequest = false;
                    HaveMinPlayers = false;
                }
            }
        }
        #region ServerStatus
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion

        #region NavigateToPage
        public ICommand NavigateToPageCommand { protected set; get; }
        public void NavigateToPage(string obj)
        {
            Page p = null;
            switch (obj)
            {
                case "Login":
                    {
                        p = new Login();
                        p.BindingContext = new LoginViewModel();
                    }
                    break;
                case "Signup":
                    {
                        p = new Signup();
                        p.BindingContext = new SignupViewModel();
                    }
                    break;
                default: break;
            }
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToLogOutPage
        public ICommand NavigateToLogOutPageCommand { protected set; get; }
        public void NavigateToLogOutPage()
        {
            Page p = new LogOut();
            p.BindingContext = new LogOutViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToCreateTeamPage
        public ICommand NavigateToCreateTeamPageCommand { protected set; get; }
        public void NavigateToCreateTeamPage()
        {
            Page p = new CreateTeam();
            p.BindingContext = new CreateTeamViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToJoinToGroupPage
        public ICommand NavigateToJoinToGroupCommand { protected set; get; }
        public void NavigateToJoinToGroupPage()
        {
            Page p = new JoinToGroup();
            p.BindingContext = new JoinToGroupViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToApproveRequestsToJoinTeamPage
        public ICommand NavigateToApproveRequestsToJoinTeamCommand { protected set; get; }
        public void NavigateToApproveRequestsToJoinTeamPage()
        {
            Page p = new ApproveRequestsToJoinTeam();
            p.BindingContext = new ApproveRequestsToJoinTeamViewModels();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToRequestToSetGamePage
        public ICommand NavigateToRequestToSetGameCommand { protected set; get; }
        public void NavigateToRequestToSetGamePage()
        {
            Page p = new RequestToSetGame();
            p.BindingContext = new RequestToSetGameViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToApproveRequestsToSetGamePage
        public ICommand NavigateToApproveRequestsToSetGameCommand { protected set; get; }
        public void NavigateToApproveRequestsToSetGamePage()
        {
            Page p = new ApproveRequestsToSetGame();
            p.BindingContext = new ApproveRequestsToSetGameViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToViewRequestToJoinTeamPage
        public ICommand NavigateToViewRequestToJoinTeamCommand { protected set; get; }
        public void NavigateToViewRequestToJoinTeamPage()
        {
            Page p = new ViewRequestToJoinTeam();
            p.BindingContext = new ViewRequestToJoinTeamViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToViewFutureGamesPage
        public ICommand NavigateToViewFutureGamesCommand { protected set; get; }
        public void NavigateToViewFutureGamesPage()
        {
            Page p = new ViewFutureGames();
            p.BindingContext = new ViewFutureGamesViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToSelectGameToEnterScorePage
        public ICommand NavigateToSelectGameToEnterScoreCommand { protected set; get; }
        public void NavigateToSelectGameToEnterScorePage()
        {
            Page p = new SelectGameToEnterScore();
            p.BindingContext = new SelectGameToEnterScoreViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToViewTeamMembersPage
        public ICommand NavigateToViewTeamMembersCommand { protected set; get; }
        public void NavigateToViewTeamMembersPage()
        {
            Team team = new Team();
            if (TheApp.CurrentCoach != null)
                team = TheApp.CurrentCoach.Team;
            else
                team = TheApp.CurrentPlayer.Team;
            Page p = new ViewTeamMembers();
            p.BindingContext = new ViewTeamMembersViewModel(team);
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToPlayersRankingPage
        public ICommand NavigateToPlayersRankingCommand { protected set; get; }
        public void NavigateToPlayersRankingPage()
        {
            Page p = new PlayersRanking();
            p.BindingContext = new PlayersRankingViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region NavigateToTeamsRankingPage
        public ICommand NavigateToTeamsRankingCommand { protected set; get; }
        public void NavigateToTeamsRankingPage()
        {
            Page p = new TeamsRanking();
            p.BindingContext = new TeamsRankingViewModel();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region LoadGames
        public async Task LoadGames()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            //if (isLoggedin)
            //{

            listGames = await proxy.GetGamesAsync(null);

            if (listGames != null)
            {
                this.ObservableCollectionGames.Clear();
                ObservableCollectionGames = new ObservableCollection<Game>(listGames.Where(g => g.GameStatusId == 3 && g.ScoreAwayTeam != -1 && g.ScoreHomeTeam != -1).ToList());
            }
            //TheApp.Leagues = await proxy.GetLeaguesAsync();
            //}
        }
        #endregion

    }
}
