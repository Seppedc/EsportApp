using EsportApp.models.GameTitleTeams;
using EsportApp.models.TeamGames;
using EsportApp.models.TornooiTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.Teams
{
    public class GetTeamModel : BaseTeamModel
    {
        public Guid Id { get; set; }

        public ICollection<GetGameTitleTeamsModel> GameTitleTeams { get; set; }
        public ICollection<GetTeamGamesModel> TeamGames { get; set; }
        public ICollection<GetTornooiTeamsModel> ToernooiTeams { get; set; }
    }
}
