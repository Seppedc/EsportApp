using EsportApp.models.TornooiGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class TornooiGameRepository : ITornooiGameRepository
    {
        public Task DeleteTornooiGame(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetTornooiGameModel> GetTornooiGame(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetTornooiGamesModel> GetTornooiGames()
        {
            throw new NotImplementedException();
        }

        public Task<GetTornooiGameModel> PostTornooiGame(PostTornooiGameModel postTornooiGameModel)
        {
            throw new NotImplementedException();
        }
    }
}
