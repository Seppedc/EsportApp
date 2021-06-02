using EsportApp.models.RefreshTokens;
using EsportApp.models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IUserRepository
    {
        Task<GetUsersModel> GetUsers();
        Task<GetUserModel> GetUser(Guid id);
        Task<GetUserModel> PostUser(PostUserModel postUserModel);
        Task PutUser(Guid id, PutUserModel putUserModel);
        Task PatchUser(Guid id, PatchUserModel patchUserModel);
        Task DeleteUser(Guid id);
        Task<List<GetRefreshTokenModel>> GetUserRefreshTokens(Guid id);

        // JWT methods

        Task<PostAuthenticateResponseModel> Authenticate(PostAuthenticateRequestModel postAuthenticateRequestModel, string ipAddress);
        Task<PostAuthenticateResponseModel> RefreshToken(string token, string ipAddress);
        Task RevokeToken(string token, string ipAddress);
    }
}
