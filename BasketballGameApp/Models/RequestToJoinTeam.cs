using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class RequestToJoinTeam
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int RequestToJoinTeamId { get; set; }

        public virtual Player Player { get; set; }
        public virtual RequestToJoinTeamStatus RequestToJoinTeamNavigation { get; set; }
        public virtual Team Team { get; set; }
    }
}
