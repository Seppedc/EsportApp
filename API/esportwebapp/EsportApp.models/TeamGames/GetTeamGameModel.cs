using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.TeamGames
{
    public class GetTeamGameModel
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }
        public string Team { get; set; }

        public Guid GameId { get; set; }
    }
}
