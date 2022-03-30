using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballGameApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BasketballGameApp.Services;

namespace BasketballGameApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesScores : ContentPage
    {
        private GamesScoresViewModel vm;
        public GamesScores()
        {
            
            
            InitializeComponent();
            vm = new GamesScoresViewModel();
            this.BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            if (this.BindingContext != null)
                await vm.LoadGames();
        }
    }
}