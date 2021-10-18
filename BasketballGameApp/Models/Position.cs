using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Position
    {
        public Position()
        {
            PlayerOnTeamForSeasons = new List<PlayerOnTeamForSeason>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<PlayerOnTeamForSeason> PlayerOnTeamForSeasons { get; set; }
    }
}
