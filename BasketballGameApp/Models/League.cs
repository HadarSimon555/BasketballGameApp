using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class League
    {
        public League()
        {
            Teams = new List<Team>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Team> Teams { get; set; }
    }
}
