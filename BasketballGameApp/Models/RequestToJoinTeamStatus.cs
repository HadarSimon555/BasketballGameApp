using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class RequestToJoinTeamStatus
    {
        public RequestToJoinTeamStatus()
        {
            RequestToJoinTeams = new List<RequestToJoinTeam>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<RequestToJoinTeam> RequestToJoinTeams { get; set; }
    }
}
