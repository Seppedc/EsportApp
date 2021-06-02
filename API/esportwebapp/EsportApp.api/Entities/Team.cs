using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class Team
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Naam { get; set; }

        
        public ICollection<GameTitleTeam> GameTitleTeams { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
        public ICollection<TeamGame> TeamGames { get; set; }
        public ICollection<TornooiTeam> ToernooiTeams { get; set; }

    }
}
