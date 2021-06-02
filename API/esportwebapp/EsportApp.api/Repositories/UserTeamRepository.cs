using EsportApp.api.Entities;
using EsportApp.models.UserTeams;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EsportApp.api.Repositories
{
    public class UserTeamRepository : IUserTeamRepository
    {
        private readonly EsportAppContext _context;

        public UserTeamRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetUserTeamsModel> GetUserTeams()
        {
            try
            {
                List<GetUserTeamModel> userTeams = await _context.UserTeams
                    .Include(x=>x.Team)
                    .OrderBy(x => x.Id)
                    .Select(x => new GetUserTeamModel
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        TeamId = x.TeamId,
                        Naam = x.Team.Naam,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (userTeams.Count == 0)
                {
                    userTeams = new List<GetUserTeamModel>();
                }
                GetUserTeamsModel getUserTeamsModel = new GetUserTeamsModel
                {
                    UserTeams = userTeams
                };
                return getUserTeamsModel;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetUserTeamModel> GetUserTeam(Guid id)
        {
            try
            {
                GetUserTeamModel userTeam = await _context.UserTeams
                    .Include(x => x.Team)
                    .OrderBy(x => x.Id)
                    .Select(x => new GetUserTeamModel
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        TeamId = x.TeamId,
                        Naam = x.Team.Naam,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                
                return userTeam;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetUserTeamModel> PostUserTeam(PostUserTeamModel postUserTeamModel)
        {
            try
            {
                EntityEntry<UserTeam> result = await _context.UserTeams.AddAsync(new UserTeam
                {
                    UserId = postUserTeamModel.UserId,
                    TeamId = postUserTeamModel.TeamId,
                });

                await _context.SaveChangesAsync();

                return await GetUserTeam(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostUserTeam 400");
            }
        }

        public async Task DeleteUserTeam(Guid id)
        {
            try
            {
                UserTeam userTeam = await _context.UserTeams
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (userTeam == null)
                {
                    throw new Exception("UserTeam niet gevonden" + this.GetType().Name + "DeleteUserTeam 404");
                }


                _context.UserTeams.Remove(userTeam);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(""+e.InnerException.Message + this.GetType().Name + "DeleteUserTeam 400");
            }
        }
    }
}

