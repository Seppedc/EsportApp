using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class TeamGame
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }

    }
}
