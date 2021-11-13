using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Coach
    {
        public Coach()
        {
            RequestGames = new List<RequestGame>();
            Teams = new List<Team>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual List<RequestGame> RequestGames { get; set; }
        public virtual List<Team> Teams { get; set; }
    }
}
