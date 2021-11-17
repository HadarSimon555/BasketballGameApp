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

namespace BasketballGameApp.ViewModels
{
    class GamesScoresViewModel : BaseViewModel
    {
        #region IsLoggedin
        private bool isLoggedin;
        public bool IsLoggedin
        {
            get => isLoggedin;
            set
            {
                isLoggedin = value;
                OnPropertyChanged("IsLoggedin");
            }
        }
        #endregion

        #region Constructor
        public GamesScoresViewModel()
        {
            App theApp = (App)App.Current;
            if (theApp.CurrentUser == null)
            {
                IsLoggedin = false;
                NavigateToPageCommand = new Command<string>(NavigateToPage);
            } 
            else
                IsLoggedin = true;
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
        public ICommand NavigateToPageCommand { protected set; get; }
        public void NavigateToPage(string obj)
        {
            Page p = null;
            switch (obj)
            {
                case "Login":
                    {
                        p = new Login();
                        p.BindingContext = new LoginViewModel();
                    }
                    break;
                case "Signup":
                    {
                        p = new Signup();
                        p.BindingContext = new SignupViewModel();
                    }
                    break;
                default: break;
            }
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion
    }
}
