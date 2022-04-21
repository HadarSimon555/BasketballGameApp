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
    class ViewFutureGamesViewModel : BaseViewModel
    {
        private App TheApp = (App)Application.Current;

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

        #region Constructor
        public ViewFutureGamesViewModel()
        {
            observableCollectionGames = new ObservableCollection<Game>();
        }
        #endregion

        #region LoadType
        private int loadType;
        public int LoadType
        {
            get
            {
                return this.loadType;
            }
            set
            {
                if (this.loadType != value)
                {
                    this.loadType = value;
                    OnPropertyChanged("LoadType");

                    if (listGames != null)
                    {
                        if (loadType == 0)
                        {
                            ObservableCollectionGames = new ObservableCollection<Game>(listGames.Where(g => g.GameStatusId == 1).ToList());
                        }
                        else
                        {
                            ObservableCollectionGames = new ObservableCollection<Game>(listGames.Where(g => g.GameStatusId == 2).ToList());
                        }
                    }

                }
            }
        }
        #endregion

        #region LoadGames
        public async Task LoadGames()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();

            listGames = await proxy.GetGamesAsync(null);

            if (listGames != null)
            {
                this.ObservableCollectionGames.Clear();
                ObservableCollectionGames = new ObservableCollection<Game>(listGames.Where(g => g.GameStatusId == 1).ToList());
            }
        }
        #endregion
    }
}
