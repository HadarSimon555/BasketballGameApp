using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Team
    {
        public Team()
        {
            Coaches = new List<Coach>();
            GameAwayTeams = new List<Game>();
            GameHomeTeams = new List<Game>();
            Players = new List<Player>();
            RequestGames = new List<RequestGame>();
            RequestToJoinTeams = new List<RequestToJoinTeam>();
        }

        public int Id { get; set; }
        public int CoachId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual Coach Coach { get; set; }
        public virtual List<Coach> Coaches { get; set; }
        public virtual List<Game> GameAwayTeams { get; set; }
        public virtual List<Game> GameHomeTeams { get; set; }
        public virtual List<Player> Players { get; set; }
        public virtual List<RequestGame> RequestGames { get; set; }
        public virtual List<RequestToJoinTeam> RequestToJoinTeams { get; set; }
    }
}
