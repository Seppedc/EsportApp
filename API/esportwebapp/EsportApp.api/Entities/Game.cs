using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class Game
    {
        public Guid Id { get; set; }

        public String Score { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Datum { get; set; }

        [Required]
        public String Status { get; set; }

        public String Type { get; set; }

        public Guid ToernooiId { get; set; }
        public Tornooi Tornooi { get; set; }

        public ICollection<UserGame> UserGames { get; set; }
        public ICollection<TeamGame> TeamGames { get; set; }

    }
}
