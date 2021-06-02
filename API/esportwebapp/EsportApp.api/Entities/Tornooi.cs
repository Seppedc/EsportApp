using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class Tornooi
    {
        public Guid Id { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Naam { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Organisator { get; set; }

        public String Beschrijving { get; set; }

        public String Type { get; set; }

        public Guid GameTitleId { get; set; }
        public GameTitle GameTitle { get; set; }

        public ICollection<TornooiTeam> TornooiTeams { get; set; }
        public ICollection<Game> Games { get; set; }

    }
}
