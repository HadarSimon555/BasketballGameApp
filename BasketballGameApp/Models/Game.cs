using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class Game
    {
        public Game()
        {
            GameStats = new List<GameStat>();
            RequestGames = new List<RequestGame>();
        }

        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int GameStatusId { get; set; }
        public int ScoreAwayTeam { get; set; }
        public int ScoreHomeTeam { get; set; }
        public string Position { get; set; }

        public virtual Team AwayTeam { get; set; }
        public virtual GameStatus GameStatus { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual List<GameStat> GameStats { get; set; }
        public virtual List<RequestGame> RequestGames { get; set; }
    }
}
