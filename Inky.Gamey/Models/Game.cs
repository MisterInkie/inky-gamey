using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inky.Gamey.Models
{
    public class Game
    {
        public Game()
        {
            Sessions = new List<Session>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
