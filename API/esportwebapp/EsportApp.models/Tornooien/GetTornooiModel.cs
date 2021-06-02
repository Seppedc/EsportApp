using EsportApp.models.Games;
using EsportApp.models.TornooiTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.Tornooien
{
    public class GetTornooiModel : BaseTornooiModel
    {
        public Guid Id { get; set; }

        public Guid GameTitleId { get; set; }
        public ICollection<GetGamesModel> Games { get; set; }
        public ICollection<GetTornooiTeamsModel> TornooiTeams { get; set; }
    }
}
