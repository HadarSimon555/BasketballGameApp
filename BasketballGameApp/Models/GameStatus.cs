using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class GameStatus
    {
        public GameStatus()
        {
            Games = new List<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
