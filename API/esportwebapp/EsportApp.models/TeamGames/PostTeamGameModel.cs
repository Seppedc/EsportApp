using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.TeamGames
{
    public class PostTeamGameModel
    {
        public Guid TeamId { get; set; }

        public Guid GameId { get; set; }
    }
}
