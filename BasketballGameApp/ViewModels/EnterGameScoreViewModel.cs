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
    class EnterGameScoreViewModel : BaseViewModel
    {
        private App theApp { get; set; }

        #region Constructor
        public EnterGameScoreViewModel(Game game)
        {
            theApp = (App)App.Current;
        }
        #endregion
    }
}
