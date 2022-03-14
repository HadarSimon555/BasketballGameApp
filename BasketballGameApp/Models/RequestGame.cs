using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class RequestGame
    {
        public int Id { get; set; }
        public int RequestGameStatusId { get; set; }
        public int CoachId { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Position { get; set; }

        public virtual Coach Coach { get; set; }
        public virtual Game Game { get; set; }
        public virtual RequestGameStatus RequestGameStatus { get; set; }
    }
}