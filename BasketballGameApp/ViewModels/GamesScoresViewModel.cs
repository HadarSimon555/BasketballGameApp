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
        #region IsLoggedin
        private bool isLoggedin;
        public bool IsLoggedIn
        {
            get => isLoggedin;
            set
            {
                isLoggedin = value;
                OnPropertyChanged("IsLoggedin");
            }
        }
        #endregion

        #region IsCoach
        private bool isCoach;
        public bool IsCoach
        {
            get => isCoach;
            set
            {
                isCoach = value;
                OnPropertyChanged("IsCoach");
            }
        }
        #endregion

        #region IsPlayer
        private bool isPlayer;
        public bool IsPlayer
        {
            get => isPlayer;
            set
            {
                isCoach = value;
                OnPropertyChanged("IsPlayer");
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
            NavigateToCreateTeamPageCommand = new Command(NavigateToCreateTeamPage);
            NavigateToJoinToGroupCommand = new Command(NavigateToJoinToGroupPage);
            observableCollectionGames = new ObservableCollection<Game>();
            App theApp = (App)App.Current;
            if (theApp.CurrentUser == null)
            {
                IsLoggedIn = !false;
            }
            else
            {
                IsLoggedIn = !true;

                if (theApp.CurrentCoach != null && TheApp.CurrentCoach.Team == null)
                {
                    IsCoach = true;
                    IsPlayer = false;
                }
                else if(TheApp.CurrentPlayer != null && TheApp.CurrentPlayer.Team == null)
                {
                    IsPlayer = true;
                    IsCoach = false;
                }
                else
                {
                    IsCoach = false;
                    IsPlayer = false;
                }    
            }
            //add server status page
            LoadGames();
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

        #region OnSubmit
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

        #region LoadGames
        public async void LoadGames()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            //if (isLoggedin)
            //{

            listGames = await proxy.GetGamesAsync();
            if (listGames != null)
            {
                foreach (Game item in listGames)
                {
                    this.observableCollectionGames.Add(item);
                }
            }
            TheApp.Leagues = await proxy.GetLeaguesAsync();
            //}
        }
        #endregion
    }
}
