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
        private Coach coach;
        public Coach Coach { get => coach; set
            {
                if (coach != value)
                {
                    coach = value;
                    OnPropertyChanged("Coach");
                }
            } }

        private App CurrentApp { get; set; }            
      
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
            CurrentApp = (App)Application.Current;
        }

        public ViewTeamMembersViewModel()
        {
            CurrentApp = (App)Application.Current;
            if (CurrentApp.CurrentPlayer != null)
            {
                SelectedTeam = CurrentApp.CurrentPlayer.Team;
                Coach = SelectedTeam.Coach;
            }
            else
            {
                SelectedTeam = CurrentApp.CurrentCoach.Team;
                Coach = CurrentApp.CurrentCoach;
            }
        }
        #endregion
    }
}
