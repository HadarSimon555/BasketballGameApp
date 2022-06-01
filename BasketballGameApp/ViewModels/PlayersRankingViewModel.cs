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
using BasketballGameApp.DTO;

namespace BasketballGameApp.ViewModels
{
    class PlayersRankingViewModel : BaseViewModel
    {
        #region ObservableCollectionGames
        private List<PlayerStatistics> listPlayerStatistics;
        private ObservableCollection<PlayerStatistics> observableCollectionPlayerStatistics;
        public ObservableCollection<PlayerStatistics> ObservableCollectionPlayerStatistics
        {
            get
            {
                return this.observableCollectionPlayerStatistics;
            }
            set
            {
                if (this.observableCollectionPlayerStatistics != value)
                {
                    this.observableCollectionPlayerStatistics = value;
                    OnPropertyChanged("ObservableCollectionPlayerStatistics");
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
            observableCollectionPlayerStatistics = new ObservableCollection<PlayerStatistics>();
        }
        #endregion

        #region LoadPlayers
        public async Task LoadPlayers()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listPlayerStatistics = await proxy.GetPlayersRankingAsync();

            if (listPlayerStatistics != null)
            {
                this.ObservableCollectionPlayerStatistics.Clear();
                ObservableCollectionPlayerStatistics = new ObservableCollection<PlayerStatistics>(listPlayerStatistics);
            }
        }
        #endregion
    }
}
