using System;
using System.Collections.Generic;

namespace BasketballGameApp.Models
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            RequestGames = new List<RequestGame>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<RequestGame> RequestGames { get; set; }
    }
}
