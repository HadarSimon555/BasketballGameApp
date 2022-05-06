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
    class PlayersRankingViewModel : BaseViewModel
    {
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
        public PlayersRankingViewModel()
        {
            observableCollectionPlayers = new ObservableCollection<Player>();
        }
        #endregion

        #region LoadPlayers
        public async Task LoadPlayers()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listPlayers = await proxy.GetPlayersAsync(null);

            if (listPlayers != null)
            {
                this.ObservableCollectionPlayers.Clear();
                ObservableCollectionPlayers = new ObservableCollection<Player>(listPlayers);
            }
        }
        #endregion
    }
}
