using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.GameTitleTeams
{
    public class PostGameTitleTeamModel
    {
        public Guid GameTitleId { get; set; }

        public Guid TeamId { get; set; }
    }
}
