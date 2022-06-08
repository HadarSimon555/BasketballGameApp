using BasketballGameApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BasketballGameApp.DTO
{
    public class TeamStatistics
    {
        public Team Team { get; set; }
        public int Games { get; set; }
        public int TotalScore { get; set; }
        public TeamStatistics() { }
    }
}
