using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Player
    {
        public Player()
        {
            GameStats = new List<GameStat>();
            PlayerOnTeamForSeasons = new List<PlayerOnTeamForSeason>();
            RequestToJoinTeams = new List<RequestToJoinTeam>();
        }

        public int Id { get; set; }
        public double Height { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual List<GameStat> GameStats { get; set; }
        public virtual List<PlayerOnTeamForSeason> PlayerOnTeamForSeasons { get; set; }
        public virtual List<RequestToJoinTeam> RequestToJoinTeams { get; set; }
    }
}
