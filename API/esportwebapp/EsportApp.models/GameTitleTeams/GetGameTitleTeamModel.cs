using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.GameTitleTeams
{
    public class GetGameTitleTeamModel
    {
        public Guid Id { get; set; }

        public Guid GameTitleId { get; set; }
        public string GameTitle { get; set; }

        public Guid TeamId { get; set; }
        public string Team { get; set; }
    }
}
