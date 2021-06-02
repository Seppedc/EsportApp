using EsportApp.models.GameTitleTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IGameTitleTeamRepository
    {
        Task<GetGameTitleTeamsModel> GetGameTitleTeams();
        Task<GetGameTitleTeamModel> GetGameTitleTeam(Guid id);
        Task<GetGameTitleTeamModel> PostGameTitleTeam(PostGameTitleTeamModel postGameTitleTeamModel);
        //Task PutGameTitleTeam(Guid id, PutGameTitleTeamModel putGameTitleTeamModel);
        Task DeleteGameTitleTeam(Guid id);
    }
}
