using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inky.Gamey.Data
{
    public class Session
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }


        public DateTime Time { get; set; }
        public string Notes { get; set; }

        public string CreatedBy { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
    }
}
