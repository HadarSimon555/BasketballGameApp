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
    class ApproveRequestsToJoinTeamViewModels : BaseViewModel
    {
        #region ObservableCollectionRequestsToJoinTeam
        private List<RequestToJoinTeam> listRequestsToJoinTeam;
        private ObservableCollection<RequestToJoinTeam> observableCollectionRequestsToJoinTeam;
        public ObservableCollection<RequestToJoinTeam> ObservableCollectionRequestsToJoinTeam
        {
            get
            {
                return this.observableCollectionRequestsToJoinTeam;
            }
            set
            {
                if (this.observableCollectionRequestsToJoinTeam != value)
                {
                    this.observableCollectionRequestsToJoinTeam = value;
                    OnPropertyChanged("ObservableCollectionRequestsToJoinTeam");
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
        public ApproveRequestsToJoinTeamViewModels()
        {
            observableCollectionRequestsToJoinTeam = new ObservableCollection<RequestToJoinTeam>();
            //SelectionChangeCommand = new Command<Team>(SelectionChangedCommand);
            LoadRequestsToJoinTeam();
        }
        #endregion

        #region LoadOpenGroups
        public async void LoadRequestsToJoinTeam()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listRequestsToJoinTeam = await proxy.GetRequestsToJoinTeamAsync(theApp.CurrentCoach);

            if (listRequestsToJoinTeam != null)
            {
                foreach (RequestToJoinTeam item in listRequestsToJoinTeam)
                {
                    this.observableCollectionRequestsToJoinTeam.Add(item);
                }
            }
        }
        #endregion
    }
}
