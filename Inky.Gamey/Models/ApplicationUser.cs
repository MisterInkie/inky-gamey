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
        }

        public ApplicationUser()
            : base()
        {
            Games = new List<Game>();
        }

        public List<Game> Games { get; set; }
    }
}
