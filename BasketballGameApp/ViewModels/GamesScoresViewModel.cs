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

        #region ObservableCollectionGames
        private App TheApp = (App)Application.Current;
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
            observableCollectionGames = new ObservableCollection<Game>();
            App theApp = (App)App.Current;

            // אם המשתמש לא מחובר
            if (theApp.CurrentUser == null)
            {
                IsLoggedIn = !true;
                IsNotLoggedIn = true;
            }
            else // אם המשתמש מחובר
            {
                IsLoggedIn = !false;
                IsNotLoggedIn = false;

                // האם המשתמש הוא מאמן ללא קבוצה
                if (theApp.CurrentCoach != null && TheApp.CurrentCoach.Team == null)
                {
                    IsCoachWithoutTeam = true;
                    IsPlayerWithoutRequest = false;
                    IsCoachWithTeam = false;
                    IsPlayerWithRequest = false;
                    HaveMinPlayers = false;
                }

                // האם המשתמש הוא שחקן שלא הגיש בקשה להצטרפות לקבוצה
                else if(TheApp.CurrentPlayer != null && TheApp.CurrentPlayer.RequestToJoinTeams.Count == 0)
                {
                    IsPlayerWithoutRequest = true;
                    IsCoachWithoutTeam = false;
                    IsCoachWithTeam = false;
                    IsPlayerWithRequest = false;
                    HaveMinPlayers = false;
                }

                // האם המשתמש הוא מאמן עם קבוצה
                else if(theApp.CurrentCoach != null && TheApp.CurrentCoach.Team != null)//לבדוק האם הקבוצה כבר מלאה
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
                else if (TheApp.CurrentPlayer != null && TheApp.CurrentPlayer.RequestToJoinTeams.Count != 0)
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
            //add server status page
            //LoadGames();
        }
        #endregion

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

        #region LoadGames
        public async Task LoadGames()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            //if (isLoggedin)
            //{

            listGames = await proxy.GetGamesAsync();

            if (listGames != null)
            {
                this.ObservableCollectionGames.Clear();
                foreach (Game item in listGames)
                {
                    this.observableCollectionGames.Add(item);
                }
            }
            //TheApp.Leagues = await proxy.GetLeaguesAsync();
            //}
        }
        #endregion
    }
}
