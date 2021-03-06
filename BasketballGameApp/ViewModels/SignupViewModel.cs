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
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "מייל לא תקין";
        public const string SHORT_PASS = "סיסמה חייבת להכיל לפחות 6 תווים";
        public const string BAD_DATE = "המשתמש חייב להיות מעל גיל 12";
        public const string BAD_HEIGHT = "המשתמש חייב להיות מעל 1.5 מטרים";
    }
    class SignupViewModel : BaseViewModel
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

        #region BirthDate
        private bool showBirthDateError;

        public bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private DateTime birthDate = DateTime.Now;

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                ValidateBirthDate();
                OnPropertyChanged("BirthDate");
            }
        }

        private string birthDateError;

        public string BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private const int MIN_AGE = 12;
        private void ValidateBirthDate()
        {
            TimeSpan ts = DateTime.Now - this.BirthDate;
            this.ShowBirthDateError = ts.TotalDays < (MIN_AGE * 365);
        }
        #endregion

        #region UserImgSrc
        private string userImgSrc;

        public string UserImgSrc
        {
            get => userImgSrc;
            set
            {
                userImgSrc = value;
                OnPropertyChanged("UserImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "defaultphoto.png";
        #endregion

        #region Height
        private double height;
        public double Height
        {
            get => height;
            set
            {
                height = value;
                ValidateHeight();
                OnPropertyChanged("Height");
            }
        }

        private string heightError;

        public string HeightError
        {
            get => heightError;
            set
            {
                heightError = value;
                OnPropertyChanged("HeightError");
            }
        }

        private const double MIN_HEIGHT = 1.5;
        private void ValidateHeight()
        {
            if (this.Height < MIN_HEIGHT)
            {
                this.ShowHeightError = true;
                //this.HeightError = ERROR_MESSAGES.BAD_HEIGHT;
            }
            else
                this.ShowHeightError = false;
            //else
            //    this.HeightError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        private bool showHeightError;
        public bool ShowHeightError
        {
            get => showHeightError;
            set
            {
                showHeightError = value;
                OnPropertyChanged("ShowHeightError");
            }
        }
        #endregion

        #region Gender
        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                ValidateGender();
                OnPropertyChanged("Gender");
            }
        }

        private string genderError;

        public string GenderError
        {
            get => genderError;
            set
            {
                genderError = value;
                OnPropertyChanged("GenderError");
            }
        }

        private void ValidateGender()
        {
            this.ShowGenderError = string.IsNullOrEmpty(Gender);
        }

        private bool showGenderError;
        public bool ShowGenderError
        {
            get => showGenderError;
            set
            {
                showGenderError = value;
                OnPropertyChanged("ShowGenderError");
            }
        }
        #endregion

        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            //else
            //    this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
            if (!this.ShowPasswordError)
            {
                if (this.Password.Length < 6)
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
                }
            }
            //else
            //    this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Player,Coach and User
        Player p;
        Coach c;
        User u;
        #endregion

        #region IsPlayer
        private bool isPlayer;
        public bool IsPlayer
        {
            get => isPlayer;
            set
            {
                isPlayer = value;
                if (isPlayer == false)
                    Height = 0;
                IsCoach = !value;
                OnPropertyChanged("IsPlayer");
            }
        }
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
        public SignupViewModel()
        {
            IsPlayer = true;
            IsCoach = false;

            App theApp = (App)App.Current;
            u = new User()
            {
                Id = 0,
                Email = "",
                Pass = "",
                BirthDate = DateTime.Now,
                Image = "",
                Gender = "",
                Name =""
            };

            //Setup default image photo
            this.UserImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen

            this.NameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.BAD_DATE;
            this.HeightError = ERROR_MESSAGES.BAD_HEIGHT;
            this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
            this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
            this.GenderError = ERROR_MESSAGES.REQUIRED_FIELD;

            this.ShowNameError = false;
            this.ShowBirthDateError = false;
            this.ShowHeightError = false;
            this.ShowGenderError = false;
            this.ShowEmailError = false;
            this.ShowPasswordError = false;

            this.SaveDataCommand = new Command(() => SaveData());
        }
        #endregion

        #region ValidateForm
        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateName();
            ValidateBirthDate();
            if (isPlayer)
                ValidateHeight();
            ValidateGender();
            ValidateEmail();
            ValidatePassword();

            //check if any validation failed
            if (ShowNameError || ShowBirthDateError || ShowGenderError || ShowEmailError || ShowPasswordError)
                return false;
            if (isPlayer && ShowHeightError)
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
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            if (ValidateForm())
            {
                if (IsPlayer)
                {
                    p = new Player()
                    {
                        User = u,
                        Height = this.Height
                    };
                }
                else
                {
                    c = new Coach()
                    {
                        User = u
                    };
                }

                this.u.Image = this.UserImgSrc;
                this.u.BirthDate = this.BirthDate;
                this.u.Gender = this.Gender;
                this.u.Email = this.Email;
                this.u.Pass = this.Password;
                this.u.Name = this.Name;

                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

                bool isEmailExist = await proxy.UserExistByEmailAsync(this.u.Email);

                if (!isEmailExist)
                {
                    Object o = null;
                    if (IsPlayer)
                    {
                        Player newPlayer = await proxy.PlayerSignUpAsync(this.p);
                        o = newPlayer;
                    }
                    else
                    {
                        Coach newCoach = await proxy.CoachSignUpAsync(this.c);
                        o = newCoach;
                    }
                    if (o == null)
                    {
                        await App.Current.MainPage.DisplayAlert("שגיאה", "שמירת המשתמש נכשלה", "בסדר");
                        await App.Current.MainPage.Navigation.PopModalAsync();
                    }
                    else
                    {
                        if (this.imageFileResult != null)
                        {
                            ServerStatus = "מעלה תמונה...";

                            if (isPlayer)
                            {
                                bool success = await proxy.UploadImage(new FileInfo()
                                {
                                    Name = this.imageFileResult.FullPath
                                }, $"{((Player)o).Id}.jpg");
                            }
                            else

                            {
                                bool success = await proxy.UploadImage(new FileInfo()
                                {
                                    Name = this.imageFileResult.FullPath
                                }, $"{((Coach)o).Id}.jpg");
                            }
                        }
                        ServerStatus = "שומר נתונים...";

                        await App.Current.MainPage.Navigation.PopAsync();
                        await App.Current.MainPage.Navigation.PopModalAsync();

                        App a = (App)App.Current;
                        a.CurrentUser = u;
                        if(o is Player)
                        {
                            Player player = (Player)o;
                            a.CurrentPlayer = player;
                        }
                        else
                        {
                            Coach coach = (Coach)o;
                            a.CurrentCoach = coach;
                        }

                        Page page = new GamesScores();
                        NavigationPage p = new NavigationPage(page);
                        NavigationPage.SetHasNavigationBar(p, false);
                        await App.Current.MainPage.DisplayAlert("הרשמה", "ההרשמה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                        await App.Current.MainPage.Navigation.PushAsync(p);
                    }
                }
                else
                {
                    if (isEmailExist)
                        await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל שהקלדת כבר קיים במערכת, בבקשה תבחר אימייל חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);
                    await App.Current.MainPage.Navigation.PopModalAsync();
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
                this.UserImgSrc = result.FullPath;
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
                this.UserImgSrc = result.FullPath;
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion
    }
}
