using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class GameTitle
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Naam { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Uitgever { get; set; }

        public ICollection<UserGameTitle> UserGameTitles { get; set; }
        public ICollection<GameTitleTeam> GameTitleTeams { get; set; }
        public ICollection<Tornooi> Toernooien { get; set; }

    }
}
