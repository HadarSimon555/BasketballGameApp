using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Team
    {
        public Team()
        {
            GameAwayTeams = new List<Game>();
            GameHomeTeams = new List<Game>();
            PlayerOnTeamForSeasons = new List<PlayerOnTeamForSeason>();
            RequestToJoinTeams = new List<RequestToJoinTeam>();
        }

        public int Id { get; set; }
        public int CoachId { get; set; }
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual Coach Coach { get; set; }
        public virtual League League { get; set; }
        public virtual List<Game> GameAwayTeams { get; set; }
        public virtual List<Game> GameHomeTeams { get; set; }
        public virtual List<PlayerOnTeamForSeason> PlayerOnTeamForSeasons { get; set; }
        public virtual List<RequestToJoinTeam> RequestToJoinTeams { get; set; }
    }
}
