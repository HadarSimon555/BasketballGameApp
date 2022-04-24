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
    }
}
