using EsportApp.api.Entities;
using EsportApp.models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EsportAppContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(EsportAppContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetRolesModel> GetRoles()
        {
            List<GetRoleModel> roles = await _context.Roles
                .OrderBy(x => x.Name)
                .Select(x => new GetRoleModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Omschrijving = x.Omschrijving
                })
                .AsNoTracking()
                .ToListAsync();



            GetRolesModel getRolesModel = new GetRolesModel
            {
                Roles = roles                  
            };

            return getRolesModel;
        }

        public async Task<GetRoleModel> GetRole(Guid id)
        {
            GetRoleModel role = await _context.Roles
                .Select(x => new GetRoleModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Omschrijving = x.Omschrijving
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (role == null)
            {
                throw new Exception("Role niet gevonden"+ this.GetType().Name+ "GetRole"+ "404");
            }

            return role;
        }

        public async Task PutRole(Guid id, PutRoleModel putRoleModel)
        {
            try
            {
                Role role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);

                if (role == null)
                {
                    throw new EntityException("Role niet gevonden"+ this.GetType().Name+ "PutRole"+ "404");
                }

                role.Omschrijving = putRoleModel.Omschrijving;

                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception(""+result.Errors.First().Description+ this.GetType().Name+ "PutRole"+ "400");
                }
            }
            catch (Exception e)
            {
                throw new Exception(""+e.InnerException.Message+ this.GetType().Name+ "PutRole"+ "400");
            }
        }
    }
}
