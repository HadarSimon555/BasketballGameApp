using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BasketballGameApp.Services;
using BasketballGameApp.Models;
using BasketballGameApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace BasketballGameApp.ViewModels
{
    class EnterGameScoreViewModel : BaseViewModel
    {
        private App theApp { get; set; }

        #region ObservableCollectionGameStats
        private List<GameStat> listGameStats;
        private ObservableCollection<GameStat> observableCollectionGameStats;
        public ObservableCollection<GameStat> ObservableCollectionGameStats
        {
            get
            {
                return this.observableCollectionGameStats;
            }
            set
            {
                if (this.observableCollectionGameStats != value)
                {
                    this.observableCollectionGameStats = value;
                    OnPropertyChanged("ObservableCollectionGameStats");
                }
            }
        }
        #endregion

        #region PlayerShots
        private int playerShots;
        public int PlayerShots
        {
            get => playerShots;
            set
            {
                playerShots = value;
                OnPropertyChanged("PlayerShots");
            }
        }
        #endregion

        #region Game
        private Game game;
        public Game Game
        {
            get => game;
            set
            {
                game = value;
                OnPropertyChanged("Game");
            }
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

        #region Constructor
        public EnterGameScoreViewModel(Game game)
        {
            ObservableCollectionGameStats = new ObservableCollection<GameStat>();
            Game = game;
            theApp = (App)App.Current;
            this.SaveDataCommand = new Command(() => SaveData());
        }
        #endregion

        #region LoadGameStats
        public async Task LoadGameStats()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            listGameStats = await proxy.GetGameStatsAsync(Game, theApp.CurrentCoach.Team);

            if (listGameStats != null)
            {
                this.ObservableCollectionGameStats.Clear();
                ObservableCollectionGameStats = new ObservableCollection<GameStat>(listGameStats);
            }
        }
        #endregion

        #region SaveData

        //The command for saving the contact
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            //foreach(GameStat x in listGameStats)
            //{
            //    x.PlayerShots = this.playerShots;
            //}

            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            bool saveGameStats = await proxy.SaveGameStatsAsync(listGameStats);
            if (!saveGameStats)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "שמירת מספר הקליעות של השחקנים נכשלה!", "בסדר");
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                ServerStatus = "קורא נתונים...";
                //theApp.CurrentCoach.RequestGames.Add(requestGame);
                await App.Current.MainPage.DisplayAlert("הכנסת תוצאות המשחק", "שמירת מספר הקליעות ש השחקנים בוצעה בהצלחה!", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();
                NavigationPage p = new NavigationPage(new GamesScores());
                NavigationPage.SetHasNavigationBar(p, false);
                await App.Current.MainPage.Navigation.PushAsync(p);
            }
        }
        #endregion
    }
}
