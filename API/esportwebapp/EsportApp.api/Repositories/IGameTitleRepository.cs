using EsportApp.models.GameTitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IGameTitleRepository
    {
        Task<GetGameTitlesModel> GetGameTitles();
        Task<GetGameTitleModel> GetGameTitle(Guid id);
        Task<GetGameTitleModel> PostGameTitle(PostGameTitleModel postGameTitleModel);
        Task PutGameTitle(Guid id, PutGameTitleModel putGameTitleModel);
        Task DeleteGameTitle(Guid id);
    }
}
