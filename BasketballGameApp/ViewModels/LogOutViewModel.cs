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
    class LogOutViewModel : BaseViewModel
    {
        #region Constructor
        public LogOutViewModel()
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

            ServerStatus = "קורא נתונים...";
            App theApp = (App)App.Current;
            theApp.CurrentUser = null;
            theApp.CurrentPlayer = null;
            theApp.CurrentCoach = null;

            await App.Current.MainPage.DisplayAlert("התנתקות", "ההתנתקות בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
            await App.Current.MainPage.Navigation.PopModalAsync();
            NavigationPage p = new NavigationPage(new GamesScores());
            NavigationPage.SetHasNavigationBar(p, false);
            await App.Current.MainPage.Navigation.PushAsync(p);

        }
        #endregion
    }
}
