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
        private Team selectedTeam;
        public Team SelectedTeam
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
            NavigateToSendRequestToGamePageCommand = new Command(NavigateToSendRequestToSetGamePage);
        }
        #endregion

        #region NavigateToSendRequestToGamePage
        public ICommand NavigateToSendRequestToGamePageCommand { protected set; get; }
        public void NavigateToSendRequestToSetGamePage()
        {
            Page p = new SendRequestToSetGame();
            p.BindingContext = new SendRequestToSetGameViewModel(selectedTeam);
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region LoadTeams
        public async Task LoadTeams()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            this.ObservableCollectionTeams.Clear();
            listTeams = await proxy.GetTeamsAsync();
            App theApp = (App)App.Current;

            if (listTeams != null)
            {
                foreach (Team item in listTeams)
                {
                    if (item.Coach.Id != theApp.CurrentCoach.Id)
                        this.ObservableCollectionTeams.Add(item);
                }
            }
        }
        #endregion
    }
}
