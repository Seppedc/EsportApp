using EsportApp.models.TeamGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface ITeamGameRepository
    {
        Task<GetTeamGamesModel> GetTeamGames();
        Task<GetTeamGameModel> GetTeamGame(Guid id);
        Task<GetTeamGameModel> PostTeamGame(PostTeamGameModel postTeamGameModel);
        //Task PutTeamGame(Guid id, PutTeamGameModel putTeamGameModel);
        Task DeleteTeamGame(Guid id);
    }
}
