using EsportApp.api.Entities;
using EsportApp.models.TornooiTeams;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class TornooiTeamRepository : ITornooiTeamRepository
    {
        private readonly EsportAppContext _context;

        public TornooiTeamRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetTornooiTeamsModel> GetTornooiTeams()
        {
            try
            {
                List<GetTornooiTeamModel> tornooiTeams = await _context.TornooiTeams
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTornooiTeamModel
                    {
                        Id = x.Id,
                        TornooiId = x.ToernooiId,
                        TeamId = x.TeamId,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (tornooiTeams.Count == 0)
                {
                    tornooiTeams = new List<GetTornooiTeamModel>();
                }
                GetTornooiTeamsModel getTornooiTeamsModel = new GetTornooiTeamsModel
                {
                    TornooiTeams = tornooiTeams
                };
                return getTornooiTeamsModel;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetTornooiTeamModel> GetTornooiTeam(Guid id)
        {
            try
            {
                GetTornooiTeamModel tornooiTeam = await _context.TornooiTeams
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTornooiTeamModel
                    {
                        Id = x.Id,
                        TornooiId = x.ToernooiId,
                        TeamId = x.TeamId,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tornooiTeam == null)
                {
                    throw new Exception("TornooiTeam niet gevonden GetTornooiTeam 404");
                }

                return tornooiTeam;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetTornooiTeamModel> PostTornooiTeam(PostTornooiTeamModel postTornooiTeamModel)
        {
            try
            {
                EntityEntry<TornooiTeam> result = await _context.TornooiTeams.AddAsync(new TornooiTeam
                {
                    ToernooiId = postTornooiTeamModel.TornooiId,
                    TeamId = postTornooiTeamModel.TeamId,
                });

                await _context.SaveChangesAsync();

                return await GetTornooiTeam(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostTornooiTeam 400");
            }
        }

        public async Task DeleteTornooiTeam(Guid id)
        {
            try
            {
                TornooiTeam tornooiTeam = await _context.TornooiTeams
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tornooiTeam == null)
                {
                    throw new Exception("TornooiTeam niet gevonden" + this.GetType().Name + "DeleteTornooiTeam 404");
                }


                _context.TornooiTeams.Remove(tornooiTeam);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "DeleteTornooiTeam 400");
            }
        }
    }
}
