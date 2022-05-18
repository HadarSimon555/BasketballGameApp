using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballGameApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BasketballGameApp.Services;
using System.Collections.ObjectModel;

namespace BasketballGameApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesScores : ContentPage
    {
        private GamesScoresViewModel vm;
        public GamesScores()
        {
            vm = new GamesScoresViewModel();
            this.BindingContext = vm;
            InitializeComponent();
           
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (this.BindingContext != null)
            {
                await vm.LoadGames();

                vm.InitButtons();
            }
        }

        private void ShowMenu(object sender, EventArgs e)
        {

        }
    }
}