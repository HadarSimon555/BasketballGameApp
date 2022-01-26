using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
