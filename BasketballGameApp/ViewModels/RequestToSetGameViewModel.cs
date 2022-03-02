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
    class RequestToSetGameViewModel : BaseViewModel
    {
        #region ObservableCollectionTeams
        private App TheApp = (App)Application.Current;
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

        #region SelectedTeam
        private string selectedTeam;
        public string SelectedTeam
        {
            get => selectedTeam;
            set
            {
                selectedTeam = value;
                OnPropertyChanged("SelectedTeam");
            }
        }
        #endregion

        #region Constructor
        public RequestToSetGameViewModel()
        {
            observableCollectionTeams = new ObservableCollection<Team>();
            //NavigateToSendRequestToGameCommand = new Command(NavigateToSendRequestToGamePage(null));
        }
        #endregion

        #region SelectionChangedCommand
        public ICommand NavigateToSendRequestToGameCommand { protected set; get; }
        public async void NavigateToSendRequestToSetGamePage(Team selectedTeam)
        {
             gdks
        }
        #endregion
    }
}
