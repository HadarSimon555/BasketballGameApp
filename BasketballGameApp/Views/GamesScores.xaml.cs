using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballGameApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasketballGameApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesScores : ContentPage
    {
        public GamesScores()
        {
            GamesScoresViewModel context = new GamesScoresViewModel();
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}