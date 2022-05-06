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
    class ViewTeamMembersViewModel : BaseViewModel
    {
        #region SelectedTeam
        private Team selectedTeam;
        public Team SelectedTeam
        {
            get
            {
                return this.selectedTeam;
            }
            set
            {
                if (this.selectedTeam != value)
                {
                    this.selectedTeam = value;
                    OnPropertyChanged("SelectedTeam");
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
        public ViewTeamMembersViewModel(Team team)
        {
            SelectedTeam = team;
        }
        #endregion
    }
}
