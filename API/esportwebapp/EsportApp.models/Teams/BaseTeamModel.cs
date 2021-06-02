using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.Teams
{
    public class BaseTeamModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum 2 en maximum 50 karakters toegelaten")]
        public string Naam { get; set; }
    }
}
