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
    class JoinToGroupViewModel : BaseViewModel
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

        #region IsPlayer
        private bool isPlayer;
        public bool IsPlayer
        {
            get => isPlayer;
            set
            {
                isPlayer = value;
                OnPropertyChanged("IsPlayer");
            }
        }
        #endregion

        #region ObservableCollectionOpenTeams
        private App TheApp = (App)Application.Current;
        private List<Team> listOpenTeams;
        private ObservableCollection<Team> observableCollectionOpenTeams;
        public ObservableCollection<Team> ObservableCollectionOpenTeams
        {
            get
            {
                return this.observableCollectionOpenTeams;
            }
            set
            {
                if (this.observableCollectionOpenTeams != value)
                {
                    this.observableCollectionOpenTeams = value;
                    OnPropertyChanged("ObservableCollectionGames");
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
        public JoinToGroupViewModel()
        {

        }
        #endregion
    }
}
