using EsportApp.models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IGameRepository
    {
        Task<GetGamesModel> GetGames();
        Task<GetGameModel> GetGame(Guid id);
        Task<GetGameModel> PostGame(PostGameModel postGameModel);
        Task PutGame(Guid id, PutGameModel putGameModel);
        Task DeleteGame(Guid id);
    }
}
