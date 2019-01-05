using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inky.Gamey.Models
{
    public class Session
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }

        public DateTime Time { get; set; }
        public string Notes { get; set; }
    }
}
