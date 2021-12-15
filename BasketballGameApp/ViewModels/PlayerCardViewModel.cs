using System.Windows.Input;
using Xamarin.Forms;
using BasketballGameApp.Services;
using BasketballGameApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using BasketballGameApp.Views;

namespace BasketballGameApp.ViewModels
{
    class PlayerCardViewModel : BaseViewModel
    {
        #region Constructor
        public PlayerCardViewModel()
        {
            currentPlayer = new PlayerOnTeamForSeason();
            //add server status page
            LoadPlayer();
        }
        #endregion

        private PlayerOnTeamForSeason currentPlayer;
        public PlayerOnTeamForSeason CurrentPlayer
        {
            get
            {
                return this.currentPlayer;
            }
            set
            {
                if (this.currentPlayer != value)
                {

                    this.currentPlayer = value;
                    OnPropertyChanged("CurrentPlayer");
                }
            }
        }

        public async void LoadPlayer()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App cur = (App)Application.Current;
            currentPlayer = await proxy.GetPlayerOnTeamForSeasonAsync(cur.CurrentUser.Id);
        }
    }
}
