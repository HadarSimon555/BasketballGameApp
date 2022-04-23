using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BasketballGameApp.Services;
using BasketballGameApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using BasketballGameApp.Views;

namespace BasketballGameApp.ViewModels
{
    class SelectGameToEnterScoreViewModel : BaseViewModel
    {
        #region ObservableCollectionGames
        private List<Game> listGames;
        private ObservableCollection<Game> observableCollectionGames;
        public ObservableCollection<Game> ObservableCollectionGames
        {
            get
            {
                return this.observableCollectionGames;
            }
            set
            {
                if (this.observableCollectionGames != value)
                {
                    this.observableCollectionGames = value;
                    OnPropertyChanged("ObservableCollectionGames");
                }
            }
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

        #region SelectedGame
        private Game selectedGame;
        public Game SelectedGame
        {
            get => selectedGame;
            set
            {
                selectedGame = value;
                OnPropertyChanged("SelectedGame");
            }
        }
        #endregion

        #region NavigateToEnterGameScorePage
        public ICommand NavigateToEnterGameScorePageCommand { protected set; get; }
        public void NavigateToEnterGameScorePage()
        {
            Page p = new EnterGameScore();
            p.BindingContext = new EnterGameScoreViewModel(selectedGame);
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        #endregion

        #region LoadGames
        public async Task LoadGames()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            listGames = await proxy.GetGamesAsync(null);

            if (listGames != null)
            {
                this.ObservableCollectionGames.Clear();
                ObservableCollectionGames = new ObservableCollection<Game>(listGames.Where(g => g.GameStatusId == 3 && ((g.HomeTeam == theApp.CurrentCoach.Team && g.ScoreHomeTeam == -1) || (g.AwayTeam == theApp.CurrentCoach.Team && g.ScoreAwayTeam == -1))).ToList());
            }
        }
        #endregion
    }
}
