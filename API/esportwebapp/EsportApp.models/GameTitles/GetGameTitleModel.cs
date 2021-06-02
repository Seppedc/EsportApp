using EsportApp.models.GameTitleTeams;
using EsportApp.models.Tornooien;
using EsportApp.models.UserGameTitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.GameTitles
{
    public class GetGameTitleModel : BaseGameTitleModel
    {
        public Guid Id { get; set; }

        public ICollection<GetGameTitleTeamModel> GameTitleTeams { get; set; }
        public ICollection<GetTornooiModel> Toernooien { get; set; }
    }
}
