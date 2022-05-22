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
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App cur = (App)Application.Current;
            CurrentPlayer = cur.CurrentPlayer;
        }
        #endregion

        private Player currentPlayer;
        public Player CurrentPlayer
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
    }
}
