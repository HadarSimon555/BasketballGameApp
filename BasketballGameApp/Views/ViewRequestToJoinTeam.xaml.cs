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
    public partial class ViewRequestToJoinTeam : ContentPage
    {
        public ViewRequestToJoinTeam()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (this.BindingContext != null)
                await ((ViewRequestToJoinTeamViewModel)this.BindingContext).LoadMyRequestToJoinTeam();
        }
    }
}