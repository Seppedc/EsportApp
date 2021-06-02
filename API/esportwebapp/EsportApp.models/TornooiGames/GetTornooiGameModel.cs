using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.TornooiGames
{
    public class GetTornooiGameModel
    {
        public Guid Id { get; set; }

        public Guid TornooiId { get; set; }
        public string Tornooi { get; set; }

        public Guid GameId { get; set; }
    }
}
