using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Entities
{
    public class Role : IdentityRole<Guid>
    {
        [StringLength(255)]
        public string Omschrijving { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
