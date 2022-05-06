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
    class TeamsRankingViewModel : BaseViewModel
    {
        #region ObservableCollectionTeams
        private List<Team> listTeams;
        private ObservableCollection<Team> observableCollectionTeams;
        public ObservableCollection<Team> ObservableCollectionTeams
        {
            get
            {
                return this.observableCollectionTeams;
            }
            set
            {
                if (this.observableCollectionTeams != value)
                {
                    this.observableCollectionTeams = value;
                    OnPropertyChanged("ObservableCollectionTeams");
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
            observableCollectionTeams = new ObservableCollection<Team>();
        }
        #endregion

        #region LoadTeams
        public async Task LoadTeams()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listTeams = await proxy.GetTeamsAsync();

            if (listTeams != null)
            {
                this.ObservableCollectionTeams.Clear();
                ObservableCollectionTeams = new ObservableCollection<Team>(listTeams);
            }
        }
        #endregion
    }
}
