using EsportApp.models.UserGameTitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IUserGameTitleRepository
    {
        Task<GetUserGameTitlesModel> GetUserGameTitles();
        Task<GetUserGameTitleModel> GetUserGameTitle(Guid id);
        Task<GetUserGameTitleModel> PostUserGameTitle(PostUserGameTitleModel postUserGameTitleModel);
        //Task PutUserTitleGame(Guid id, PutUserGameTitleModel putUserGameTitleModel);
        Task DeleteUserGameTitle(Guid id);
    }
}
