using EsportApp.models.UserTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IUserTeamRepository
    {
        Task<GetUserTeamsModel> GetUserTeams();
        Task<GetUserTeamModel> GetUserTeam(Guid id);
        Task<GetUserTeamModel> PostUserTeam(PostUserTeamModel postUserTeamModel);
        //Task PutUserTeam(Guid id, PutUserTeamModel putUserTeamModel);
        Task DeleteUserTeam(Guid id);
    }
}
