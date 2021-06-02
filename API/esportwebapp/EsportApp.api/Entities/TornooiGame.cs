using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class TornooiGame
    {
        public Guid Id { get; set; }

        public Guid ToernooiId { get; set; }
        public Tornooi Toernooi { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
