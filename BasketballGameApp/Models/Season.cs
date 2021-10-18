using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Season
    {
        public Season()
        {
            Games = new List<Game>();
            PlayerOnTeamForSeasons = new List<PlayerOnTeamForSeason>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Game> Games { get; set; }
        public virtual List<PlayerOnTeamForSeason> PlayerOnTeamForSeasons { get; set; }
    }
}
