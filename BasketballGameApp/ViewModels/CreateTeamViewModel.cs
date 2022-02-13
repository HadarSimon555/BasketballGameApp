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

        #region Coach
        private Coach coach;
        public Coach Coach
        {
            get => coach;
            set
            {
                coach = value;
                OnPropertyChanged("Coach");
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
        //private bool isCoach;
        //public bool IsCoach
        //{
        //    get => isCoach;
        //    set
        //    {
        //        isCoach = value;
        //        OnPropertyChanged("IsCoach");
        //    }
        //}
        #endregion

        #region SelectedLeague
        private League selectedLeague;
        public League SelectedLeague
        {
            get => selectedLeague;
            set
            {
                selectedLeague = value;
                OnPropertyChanged("SelectedLeague");
            }
        }
        #endregion

        //public ObservableCollection<League> Leagues { get; set; }
        private App theApp { get; set; }
        private Team team { get; set; }

        #region Constructor
        public CreateTeamViewModel()
        {
            theApp = (App)App.Current;
            //this.Leagues = new ObservableCollection<League>(theApp.Leagues);

            team = new Team()
            {
                //League = null,
                CoachId = 0,
                Name = string.Empty,
                Image = string.Empty
            };

            //Setup default image photo
            this.TeamImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen

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
                team = new Team()
                {
                    //League = this.SelectedLeague,
                    //Coach = new Coach() { User = theApp.CurrentUser },
                    Coach = theApp.CurrentCoach,
                    Name = this.Name,
                    Image = this.TeamImgSrc
                };

                theApp.CurrentCoach.Team = team;
                this.team.Image = this.TeamImgSrc;
                this.team.Name = this.Name;
                //this.team.League = this.SelectedLeague;
                //this.team.Coach = this.Coach;

                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

                Team newTeam = await proxy.AddTeamAsync(this.team);

                if (newTeam == null)
                {
                    theApp.CurrentCoach.Team = null;
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת הקבוצה נכשלה", "בסדר");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }

                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";
                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{(newTeam).Id}.jpg");
                    }

                    ServerStatus = "שומר נתונים...";

                    //close the message and add contact windows!
                    await App.Current.MainPage.Navigation.PopAsync();
                    await App.Current.MainPage.Navigation.PopModalAsync();

                    theApp = (App)App.Current;
                    //theApp.CurrentCoach.Team = team;
                    Page page = new GamesScores();
                    await App.Current.MainPage.Navigation.PushAsync(page);
                    await App.Current.MainPage.DisplayAlert("יצירת קבוצה", "יצירת הקבוצה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }
        #endregion

        #region OnPickImage
        //The following command handle the pick photo button
        FileResult imageFileResult;

        public event Action<ImageSource> SetImageSourceEvent;
        public ICommand PickImageCommand => new Command(OnPickImage);
        public async void OnPickImage()
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "Pick a photo"
            });

            if (result != null)
            {
                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                this.TeamImgSrc = result.FullPath;
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion

        #region OnCameraImage
        //The following command handle the take photo button
        public ICommand CameraImageCommand => new Command(OnCameraImage);
        public async void OnCameraImage()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "Take a photo"
            });

            if (result != null)
            {
                this.imageFileResult = result;
                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                this.TeamImgSrc = result.FullPath;
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion
    }
}
