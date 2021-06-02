using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class GameTitleTeam
    {
        public Guid Id { get; set; }

        public Guid GameTitleId { get; set; }
        public GameTitle GameTitle { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }
}
