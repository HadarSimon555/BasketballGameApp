using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Position
    {
        public Position()
        {
            Players = new List<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Player> Players { get; set; }
    }
}
