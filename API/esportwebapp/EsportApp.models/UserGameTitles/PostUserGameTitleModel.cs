using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.UserGameTitles
{
    public class PostUserGameTitleModel
    {
        public Guid UserId { get; set; }

        public Guid GameTitleId { get; set; }
    }
}
