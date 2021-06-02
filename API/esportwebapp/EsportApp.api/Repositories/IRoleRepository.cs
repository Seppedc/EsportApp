using EsportApp.models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public interface IRoleRepository
    {
        Task<GetRolesModel> GetRoles();
        Task<GetRoleModel> GetRole(Guid id);
        Task PutRole(Guid id, PutRoleModel putRoleModel);
    }
}
