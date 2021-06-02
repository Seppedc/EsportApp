using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.UserGames
{
    public class PostUserGameModel
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }
    }
}
