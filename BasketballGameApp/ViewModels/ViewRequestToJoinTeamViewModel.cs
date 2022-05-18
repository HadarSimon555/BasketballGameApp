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
    class ViewRequestToJoinTeamViewModel : BaseViewModel
    {
        #region MyRequestToJoinTeam
        private RequestToJoinTeam myRequestToJoinTeam;
        public RequestToJoinTeam MyRequestToJoinTeam
        {
            get
            {
                return this.myRequestToJoinTeam;
            }
            set
            {
                if (this.myRequestToJoinTeam != value)
                {
                    this.myRequestToJoinTeam = value;
                    OnPropertyChanged("MyRequestToJoinTeam");
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

        #region Constructor
        public ViewRequestToJoinTeamViewModel()
        {
            MyRequestToJoinTeam = new RequestToJoinTeam();
        }
        #endregion

        #region LoadMyRequestToJoinTeam
        public async Task LoadMyRequestToJoinTeam()
        {
            BasketballGameAPIProxy proxy = BasketballGameAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

           MyRequestToJoinTeam = await proxy.GetMyRequestToJoinTeamAsync(theApp.CurrentPlayer);

           
        }
        #endregion
    }
}
