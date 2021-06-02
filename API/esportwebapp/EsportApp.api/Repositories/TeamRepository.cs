using EsportApp.api.Entities;
using EsportApp.models.Teams;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;

namespace EsportApp.api.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly EsportAppContext _context;

        public TeamRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetTeamsModel> GetTeams()
        {
            try
            {
                List<GetTeamModel> teams = await _context.Teams
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTeamModel
                    {
                        Id = x.Id,
                        Naam = x.Naam,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (teams.Count == 0)
                {
                    teams = new List<GetTeamModel>();
                }
                GetTeamsModel getTeamsModel = new GetTeamsModel
                {
                    Teams = teams
                };
                return getTeamsModel;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetTeamModel> GetTeam(Guid id)
        {
            try
            {
                GetTeamModel team = await _context.Teams
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTeamModel
                    {
                        Id = x.Id,
                        Naam = x.Naam,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (team == null)
                {
                    throw new Exception("Team niet gevonden GetTeam 404");
                }

                return team;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }
        public async Task PutTeam(Guid id, PutTeamModel putTeamModel)
        {
            try
            {
                Team team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

                if (team == null)
                {
                    throw new Exception("team niet gevonden" + this.GetType().Name + "PutTeam 404");
                }

                team.Naam = putTeamModel.Naam;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "PutTeam 400");
            }
        }

        public async Task<GetTeamModel> PostTeam(PostTeamModel postTeamModel)
        {
            try
            {
                EntityEntry<Team> result = await _context.Teams.AddAsync(new Team
                {
                    Naam = postTeamModel.Naam,
                });

                await _context.SaveChangesAsync();

                return await GetTeam(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostTeam 400");
            }
        }

        public async Task DeleteTeam(Guid id)
        {
            try
            {
                Team team = await _context.Teams
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (team == null)
                {
                    throw new Exception("Team niet gevonden" + this.GetType().Name + "DeleteTeam 404");
                }

                List<GameTitleTeam> gameTitleTeams = await _context.GameTitleTeams.Where(x => x.TeamId == team.Id).ToListAsync();
                _context.GameTitleTeams.RemoveRange(gameTitleTeams);
                List<UserTeam> UserTeam = await _context.UserTeams.Where(x => x.TeamId == team.Id).ToListAsync();
                _context.UserTeams.RemoveRange(UserTeam);
                List<TeamGame> teamGames = await _context.TeamGames.Where(x => x.TeamId == team.Id).ToListAsync();
                _context.TeamGames.RemoveRange(teamGames);

                _context.Teams.Remove(team);  

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "DeleteTeam 400");
            }
        }
    }
}
