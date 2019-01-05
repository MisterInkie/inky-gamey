using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inky.Gamey.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName)
            : base(userName)
        {
            Games = new List<Game>();
            Sessions = new List<Session>();
        }

        public ApplicationUser()
            : base()
        {
            Games = new List<Game>();
            Sessions = new List<Session>();
        }

        public List<Game> Games { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
