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
using Xamarin.Essentials;
using System.Linq;
using BasketballGameApp.DTO;

namespace BasketballGameApp.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        #region Email
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        #endregion

        #region Password
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
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

        #region OnSubmit
        public ICommand SubmitCommand { protected set; get; }
        public async void OnSubmit()
        {
            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            User user = await proxy.LoginAsync(new UserDTO() { Email = this.Email, Pass = this.Password }); ;
            if (user == null)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק אימייל וסיסמה ונסה שוב", "בסדר");
            }
            else
            {
                ServerStatus = "קורא נתונים...";
                App theApp = (App)App.Current;
                theApp.CurrentUser = user;

                if (user.Coaches.Count > 0)
                {
                    theApp.CurrentCoach = user.Coaches[0];
                    theApp.CurrentPlayer = null;
                }
                    
                if (user.Players.Count > 0)
                {
                    theApp.CurrentPlayer = user.Players[0];
                    theApp.CurrentCoach = null;
                }
                    
                await App.Current.MainPage.DisplayAlert("התחברות", "ההתחברות בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();
                NavigationPage p = new NavigationPage(new GamesScores());
                NavigationPage.SetHasNavigationBar(p, false);
                await App.Current.MainPage.Navigation.PushAsync(p);
            }
        }
        #endregion
    }
}
