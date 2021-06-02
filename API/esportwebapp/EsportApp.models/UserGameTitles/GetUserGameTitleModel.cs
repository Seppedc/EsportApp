using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.UserGameTitles
{
    public class GetUserGameTitleModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string User { get; set; }
        public string Naam { get; set; }

        public Guid GameTitleId { get; set; }
        public string GameTitle { get; set; }
    }
}
