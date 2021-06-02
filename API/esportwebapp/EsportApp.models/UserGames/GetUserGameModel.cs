using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.UserGames
{
    public class GetUserGameModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string User { get; set; }

        public Guid GameId { get; set; }
    }
}
