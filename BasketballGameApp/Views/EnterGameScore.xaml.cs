using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballGameApp.Services;
using BasketballGameApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasketballGameApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterGameScore : ContentPage
    {
        public EnterGameScore()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (this.BindingContext != null)
                await ((EnterGameScoreViewModel)this.BindingContext).LoadGameStats();
        }
    }
}