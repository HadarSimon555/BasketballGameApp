using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BasketballGameApp.Services;
using BasketballGameApp.Models;
using Xamarin.Essentials;
using System.Linq;
using BasketballGameApp.DTO;

namespace BasketballGameApp.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
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
        public ICommand SubmitCommand { protected set; get; }

        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

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

        public async void OnSubmit()
        {
            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            User user = await proxy.LoginAsync(new UserDTO() { Email = this.Email, Pass = this.Password }); ;
            if (user == null)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
            }
            else
            {
                ServerStatus = "קורא נתונים...";
                App theApp = (App)App.Current;
                theApp.CurrentUser = user;
                await App.Current.MainPage.DisplayAlert("התחברות", "ההתחברות בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
            }
        }
    }
}
