using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Player
    {
        public Player()
        {
            GameStats = new List<GameStat>();
            RequestToJoinTeams = new List<RequestToJoinTeam>();
        }

        public int Id { get; set; }
        public double Height { get; set; }
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public int TeamId { get; set; }

        public virtual Position Position { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
        public virtual List<GameStat> GameStats { get; set; }
        public virtual List<RequestToJoinTeam> RequestToJoinTeams { get; set; }
    }
}
