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
    class CreateTeamViewModel : BaseViewModel
    {
        #region Name
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }

        private bool showNameError;
        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }
        #endregion

        #region TeamImgSrc
        private string teamImgSrc;

        public string TeamImgSrc
        {
            get => teamImgSrc;
            set
            {
                teamImgSrc = value;
                OnPropertyChanged("TeamImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "defaultphoto.png";
        #endregion

        #region IsCoach
        private bool isCoach;
        public bool IsCoach
        {
            get => isCoach;
            set
            {
                isCoach = value;
                OnPropertyChanged("IsCoach");
            }
        }
        #endregion

        #region Constructor
        public CreateTeamViewModel()
        {
            App theApp = (App)App.Current;
            Team team = new Team()
            {
                Id = 0,
                CoachId = 0,
                LeagueId = 0,
                Name = "",
                Image = ""
            };

            //Setup default image photo
            this.TeamImgSrc = DEFAULT_PHOTO_SRC;
            //this.imageFileResult = null; //mark that no picture was chosen

            this.NameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShowNameError = false;

            this.SaveDataCommand = new Command(() => SaveData());
        }
        #endregion

        #region ValidateForm
        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateName();

            //check if any validation failed
            if (ShowNameError)
                return false;
            return true;
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
            if (ValidateForm())
            {

            }
        }
    }
    #endregion
}
