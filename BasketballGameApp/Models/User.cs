using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class User
    {
        public User()
        {
            Coaches = new List<Coach>();
            Players = new List<Player>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }

        public virtual List<Coach> Coaches { get; set; }
        public virtual List<Player> Players { get; set; }
    }
}
