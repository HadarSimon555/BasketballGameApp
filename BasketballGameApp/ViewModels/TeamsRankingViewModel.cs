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
    class TeamsRankingViewModel : BaseViewModel
    {
        #region ObservableCollectionTeams
        private List<TeamStatistics> listTeamStatistics;
        private ObservableCollection<TeamStatistics> observableCollectionTeamStatistics;
        public ObservableCollection<TeamStatistics> ObservableCollectionTeamStatistics
        {
            get
            {
                return this.observableCollectionTeamStatistics;
            }
            set
            {
                if (this.observableCollectionTeamStatistics != value)
                {
                    this.observableCollectionTeamStatistics = value;
                    OnPropertyChanged("ObservableCollectionTeamStatistics");
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
        public TeamsRankingViewModel()
        {
            observableCollectionTeamStatistics = new ObservableCollection<TeamStatistics>();
        }
        #endregion

        #region LoadTeams
        public async Task LoadTeams()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listTeamStatistics = await proxy.GetTeamsRankingAsync();

            if (listTeamStatistics != null)
            {
                this.ObservableCollectionTeamStatistics.Clear();
                ObservableCollectionTeamStatistics = new ObservableCollection<TeamStatistics>(listTeamStatistics);
            }
        }
        #endregion
    }
}
