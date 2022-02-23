﻿using System;
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
        #endregion

        #region ApproveRequest
        public ICommand ApproveCommand { get; protected set; }
        private async void ApproveRequest(RequestToJoinTeam request)
        {
            App theApp = (App)App.Current;
            Player player = request.Player;
            player.Team = request.Team;
            player.
            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            bool updatePlayer = await proxy.UpdatePlayerAsync(player);

            if (!updatePlayer)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת השחקן לקבוצה נכשלה!", "בסדר");
            }
            else
            {
                ServerStatus = "קורא נתונים...";
                
                theApp.CurrentPlayer = player;
                await App.Current.MainPage.DisplayAlert("התחברות", "הוספת השחקן לקבוצה בוצעה בהצלחה!", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();
                NavigationPage p = new NavigationPage(new GamesScores());
                NavigationPage.SetHasNavigationBar(p, false);
                await App.Current.MainPage.Navigation.PushAsync(p);
            }

        }
        #endregion

        #region DeleteRequest
        public ICommand DeleteCommand { get; protected set; }
        private async void DeleteRequest(RequestToJoinTeam obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
