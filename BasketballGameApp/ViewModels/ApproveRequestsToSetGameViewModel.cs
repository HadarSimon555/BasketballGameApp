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
    class ApproveRequestsToSetGameViewModel : BaseViewModel
    {
        #region ObservableCollectionRequestsGame
        private List<RequestGame> listRequestsGame;
        private ObservableCollection<RequestGame> observableCollectionRequestsGame;
        public ObservableCollection<RequestGame> ObservableCollectionRequestsGame
        {
            get
            {
                return this.observableCollectionRequestsGame;
            }
            set
            {
                if (this.observableCollectionRequestsGame != value)
                {
                    this.observableCollectionRequestsGame = value;
                    OnPropertyChanged("ObservableCollectionRequestsGame");
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
        public ApproveRequestsToSetGameViewModel()
        {
            ObservableCollectionRequestsGame = new ObservableCollection<RequestGame>();
            ApproveCommand = new Command<RequestGame>(ApproveRequest);
            DeleteCommand = new Command<RequestGame>(DeleteRequest);
        }
        #endregion

        #region LoadRequestsGame
        public async Task LoadRequestsGame()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listRequestsGame = await proxy.GetRequestsGameAsync(theApp.CurrentCoach);

            if (listRequestsGame != null)
            {
                //foreach (RequestToJoinTeam item in listRequestsToJoinTeam)
                //{
                //    this.ObservableCollectionRequestsToJoinTeam.Add(item);
                //    OnPropertyChanged("ObservableCollectionRequestsToJoinTeam");
                //}
                ObservableCollectionRequestsGame = new ObservableCollection<RequestGame>(listRequestsGame);
            }
        }
        #endregion

        #region ApproveRequest
        public ICommand ApproveCommand { get; protected set; }
        private async void ApproveRequest(RequestGame request)
        {

        }
        #endregion

        #region DeleteRequest
        public ICommand DeleteCommand { get; protected set; }
        private async void DeleteRequest(RequestGame request)
        {
          
        }
        #endregion
    }
}
