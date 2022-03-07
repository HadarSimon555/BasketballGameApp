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
        private TimePicker time = new TimePicker
        {
            Time = new TimeSpan(0, 0, 0)
        };
        public TimePicker Time
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

        private App theApp { get; set; }
        private RequestGame requestGame { get; set; }

        #region Constructor
        public SendRequestToSetGameViewModel()
        {
            theApp = (App)App.Current;

            requestGame = new RequestGame
            {
                RequestStatusId = 0,
                CoachId = 0,
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
                Coach = theApp.CurrentCoach,

            };
        }
        #endregion
    }
}
