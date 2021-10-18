using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketballGameApp.Models;


namespace BasketballGameApp.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Pass { get; set; }
        public UserDTO() { }
        public UserDTO(User user)
        {
            Email = user.Email;
            Pass = user.Pass;
        }
    }
}
