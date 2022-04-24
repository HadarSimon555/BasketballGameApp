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

        #region ObservableCollectionPlayers
        private List<Player> listPlayers;
        private ObservableCollection<Player> observableCollectionPlayers;
        public ObservableCollection<Player> ObservableCollectionPlayers
        {
            get
            {
                return this.observableCollectionPlayers;
            }
            set
            {
                if (this.observableCollectionPlayers != value)
                {
                    this.observableCollectionPlayers = value;
                    OnPropertyChanged("ObservableCollectionPlayers");
                }
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
            ObservableCollectionPlayers = new ObservableCollection<Player>();
            theApp = (App)App.Current;
            this.SaveDataCommand = new Command(() => SaveData());
        }
        #endregion

        #region LoadPlayers
        public async Task LoadPlayers()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            listPlayers = await proxy.GetPlayersAsync(theApp.CurrentUser);

            if (listPlayers != null)
            {
                this.ObservableCollectionPlayers.Clear();
                ObservableCollectionPlayers = new ObservableCollection<Player>(listPlayers);
            }
        }
        #endregion

        #region SaveData
        //This event is fired after the new contact is generated in the system so it can be added to the list of contacts
        //public event Action<Player, Player> PlayerUpdatedEvent;

        //The command for saving the contact
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            foreach(Player p in listPlayers)
            {

            }

            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            bool savePlayersShots = await proxy.SavePlayerShotsAsync(listPlayers);
            if (!savePlayersShots)
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
