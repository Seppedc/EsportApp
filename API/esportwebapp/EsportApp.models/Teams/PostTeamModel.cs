using EsportApp.models.GameTitleTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.Teams
{
    public class PostTeamModel : BaseTeamModel
    {
        public ICollection<GetGameTitleTeamsModel> GameTitleTeams { get; set; }
    }
}
