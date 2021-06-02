using EsportApp.models.Tornooien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface ITornooiRepository
    {
        Task<GetTornooienModel> GetTornooien();
        Task<GetTornooiModel> GetTornooi(Guid id);
        Task<GetTornooiModel> PostTornooi(PostTornooiModel postTornooiModel);
        Task PutTornooi(Guid id, PutTornooiModel putTornooiModel);
        Task DeleteTornooi(Guid id);
    }
}
