using EsportApp.models.UserGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IUserGameRepository
    {
        Task<GetUserGamesModel> GetUserGames();
        Task<GetUserGamesModel> GetUserGames(Guid id);
        Task<GetUserGameModel> GetUserGame(Guid id);
        Task<GetUserGameModel> PostUserGame(PostUserGameModel postUserGameModel);
        //Task PutUserGame(Guid id, PutUserGameModel putUserGameModel);
        Task DeleteUserGame(Guid id);
    }
}
