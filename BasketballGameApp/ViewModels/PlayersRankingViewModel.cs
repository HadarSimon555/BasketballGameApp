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
        private IDictionary<Player, double> dictionaryPlayers;
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

        #region ObservableCollectionValues
        private ObservableCollection<double> observableCollectionValues;
        public ObservableCollection<double> ObservableCollectionValues
        {
            get
            {
                return this.observableCollectionValues;
            }
            set
            {
                if (this.observableCollectionValues != value)
                {
                    this.observableCollectionValues = value;
                    OnPropertyChanged("ObservableCollectionValues");
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

            dictionaryPlayers = await proxy.GetPlayersRankingAsync();

            if (dictionaryPlayers != null)
            {
                this.ObservableCollectionPlayers.Clear();
                this.ObservableCollectionValues.Clear();
                ObservableCollectionPlayers = new ObservableCollection<Player>(dictionaryPlayers.Keys);
                ObservableCollectionValues = new ObservableCollection<double>(dictionaryPlayers.Values);
            }
        }
        #endregion
    }
}
