using EsportApp.models.GameTitleTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.GameTitles
{
    public class PostGameTitleModel : BaseGameTitleModel
    {
        public ICollection<GetGameTitleTeamModel> GameTitleTeams { get; set; }
    }
}
