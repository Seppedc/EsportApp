using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class TornooiTeam
    {
        public Guid Id { get; set; }

        public Guid ToernooiId { get; set; }
        public Tornooi Toernooi { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }
}
