using EsportApp.models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface ITeamRepository
    {
        Task<GetTeamsModel> GetTeams();
        Task<GetTeamModel> GetTeam(Guid id);
        Task<GetTeamModel> PostTeam(PostTeamModel postTeamModel);
        Task PutTeam(Guid id, PutTeamModel putTeamModel);
        Task DeleteTeam(Guid id);
    }
}
