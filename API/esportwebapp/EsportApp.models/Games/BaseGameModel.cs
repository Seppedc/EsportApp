using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.Games
{
    public class BaseGameModel
    {
        public string Score { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        public string Status { get; set; }

        public string Type { get; set; }

    }
}
