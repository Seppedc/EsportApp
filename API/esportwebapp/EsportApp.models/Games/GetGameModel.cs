using EsportApp.models.TeamGames;
using EsportApp.models.Teams;
using EsportApp.models.TornooiGames;
using EsportApp.models.UserGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.Games
{
    public class GetGameModel : BaseGameModel
    {
        public Guid Id { get; set; }
        public Guid TornooiId { get; set; }
        public string Tornooi { get; set; }
        public ICollection<string> Teams { get; set; }
        public ICollection<GetUserGameModel> UserGames { get; set; }
        public ICollection<GetTeamGameModel> TeamGames { get; set; }

    }
}
