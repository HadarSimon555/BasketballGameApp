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
            ObservableCollectionRequestsToJoinTeam = new ObservableCollection<RequestToJoinTeam>();
            //SelectionChangeCommand = new Command<Team>(SelectionChangedCommand);
            //LoadRequestsToJoinTeam();
            ApproveCommand = new Command<RequestToJoinTeam>(ApproveRequest);
            DeleteCommand= new Command<RequestToJoinTeam>(DeleteRequest);
        }

        private async void DeleteRequest(RequestToJoinTeam obj)
        {
            throw new NotImplementedException();
        }

        private async void ApproveRequest(RequestToJoinTeam obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LoadRequestsToJoinTeam
        public async Task LoadRequestsToJoinTeam()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listRequestsToJoinTeam = await proxy.GetRequestsToJoinTeamAsync(theApp.CurrentCoach);

            if (listRequestsToJoinTeam != null)
            {
                //foreach (RequestToJoinTeam item in listRequestsToJoinTeam)
                //{
                //    this.ObservableCollectionRequestsToJoinTeam.Add(item);
                //    OnPropertyChanged("ObservableCollectionRequestsToJoinTeam");
                //}
                ObservableCollectionRequestsToJoinTeam = new ObservableCollection<RequestToJoinTeam>(listRequestsToJoinTeam);
            }
        }
        public ICommand ApproveCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }
        #endregion
    }
}
