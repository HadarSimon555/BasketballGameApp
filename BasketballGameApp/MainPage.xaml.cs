using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BasketballGameApp.Services;

namespace BasketballGameApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            BasketballGameAPIProxy proxy =  BasketballGameAPIProxy.CreateProxy();
          lbl_txt.Text= await proxy.GetHello();
        }
    }
}
