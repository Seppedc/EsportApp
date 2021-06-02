using EsportApp.models.TornooiGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface ITornooiGameRepository
    {
        Task<GetTornooiGamesModel> GetTornooiGames();
        Task<GetTornooiGameModel> GetTornooiGame(Guid id);
        Task<GetTornooiGameModel> PostTornooiGame(PostTornooiGameModel postTornooiGameModel);
        //Task PutTornooiGame(Guid id, PutTornooiGameModel putTornooiGameModel);
        Task DeleteTornooiGame(Guid id);
    }
}
