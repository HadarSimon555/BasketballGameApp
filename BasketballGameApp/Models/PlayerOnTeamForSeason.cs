using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class PlayerOnTeamForSeason
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public int PositionId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Position Position { get; set; }
        public virtual Season Season { get; set; }
        public virtual Team Team { get; set; }
    }
}
