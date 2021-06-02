using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.TornooiTeams
{
    public class PostTornooiTeamModel
    {
        public Guid TornooiId { get; set; }

        public Guid TeamId { get; set; }
    }
}
