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
    public partial class CreateTeam : ContentPage
    {
        public CreateTeam()
        {
            CreateTeamViewModel vm = new CreateTeamViewModel();
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}