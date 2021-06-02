using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EsportApp.api.Entities
{
    public class User: IdentityUser<Guid>
    {
        [Required]
        [StringLength(45, MinimumLength = 2)]
        public string Voornaam { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Familienaam { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public override string Email { get; set; }

        [Required]
        public int Punten { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
        public ICollection<UserGame> UserGames { get; set; }
        public ICollection<UserGameTitle> UserGameTitles { get; set; }


    }
}
