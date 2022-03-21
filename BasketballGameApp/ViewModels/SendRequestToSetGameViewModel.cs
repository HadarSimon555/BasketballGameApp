using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BasketballGameApp.Services;
using BasketballGameApp.Models;
using BasketballGameApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace BasketballGameApp.ViewModels
{
    class SendRequestToSetGameViewModel : BaseViewModel
    {
        public DateTime MinDate { get => DateTime.Now; }

        public DateTime MaxDate { get => DateTime.Now.AddMonths(6); }

        #region Date
        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        #endregion

        #region Time
        private TimeSpan time = new TimeSpan(0, 0, 0);

        public TimeSpan Time
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }
        #endregion

        #region Position
        private string position;
        public string Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged("Position");
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

        private App theApp { get; set; }
        private RequestGame requestGame { get; set; }

        #region Constructor
        public SendRequestToSetGameViewModel(Team team)
        {
            theApp = (App)App.Current;
            SelectedTeam = team;
            requestGame = new RequestGame
            {
                RequestGameStatusId = 0,
                CoachHomeTeamId = 0,
                AwayTeamId = 0,
                GameId = 0,
                Date = DateTime.Now,
                Time = new TimeSpan(0, 0, 0),
                Position = string.Empty
            };

            this.SaveDataCommand = new Command(() => SaveData());
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

        #region SaveData
        //This event is fired after the new contact is generated in the system so it can be added to the list of contacts
        //public event Action<Player, Player> PlayerUpdatedEvent;

        //The command for saving the contact
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            requestGame = new RequestGame
            {
                CoachHomeTeam = theApp.CurrentCoach,
                AwayTeam = SelectedTeam,
                Time = this.Time,
                Date = this.Date,
                Position = this.Position
            };

            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            bool HasGame = await proxy.HasGameAsync(SelectedTeam.Id, date);

            if (!HasGame)
            {
                bool addRequest = await proxy.AddRequestToGameAsync(requestGame);
                if (!addRequest)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הגשת הבקשה לקביעת משחק נכשלה!", "בסדר");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    ServerStatus = "קורא נתונים...";
                    theApp.CurrentCoach.RequestGames.Add(requestGame);
                    await App.Current.MainPage.DisplayAlert("שליחת בקשה למשחק", "הגשת הבקשה לקביעת משחק נשלחה למאמן הקבוצה המתחרה!", "אישור", FlowDirection.RightToLeft);
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    NavigationPage p = new NavigationPage(new GamesScores());
                    NavigationPage.SetHasNavigationBar(p, false);
                    await App.Current.MainPage.Navigation.PushAsync(p);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "לאחת מהקבוצות כבר יש משחק בתאריך זה, בחר תאריך חדש לקביעת משחק!", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }
        #endregion
    }
}
