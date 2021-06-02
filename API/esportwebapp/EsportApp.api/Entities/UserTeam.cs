using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class UserTeam
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }
}
