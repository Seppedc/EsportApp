using EsportApp.models.TornooiTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface ITornooiTeamRepository
    {
        Task<GetTornooiTeamsModel> GetTornooiTeams();
        Task<GetTornooiTeamModel> GetTornooiTeam(Guid id);
        Task<GetTornooiTeamModel> PostTornooiTeam(PostTornooiTeamModel postTornooiTeamModel);
        //Task PutTornooiTeam(Guid id, PutTornooiTeamModel putTornooiTeamModel);
        Task DeleteTornooiTeam(Guid id);
    }
}
