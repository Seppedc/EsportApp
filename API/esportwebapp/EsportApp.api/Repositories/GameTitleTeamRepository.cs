using EsportApp.api.Entities;
using EsportApp.models.GameTitleTeams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class GameTitleTeamRepository : IGameTitleTeamRepository
    {
        private readonly EsportAppContext _context;

        public GameTitleTeamRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetGameTitleTeamsModel> GetGameTitleTeams()
        {
            try
            {
                List<GetGameTitleTeamModel> gameTitleTeams = await _context.GameTitleTeams
                    .OrderBy(x => x.Id)
                    .Select(x => new GetGameTitleTeamModel
                    {
                        Id = x.Id,
                        GameTitleId = x.GameTitleId,
                        TeamId = x.TeamId,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (gameTitleTeams.Count == 0)
                {
                    gameTitleTeams = new List<GetGameTitleTeamModel>();
                }
                GetGameTitleTeamsModel getGameTitleTeamsModel = new GetGameTitleTeamsModel
                {
                    GameTitleTeams = gameTitleTeams
                };
                return getGameTitleTeamsModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetGameTitleTeamModel> GetGameTitleTeam(Guid id)
        {
            try
            {
                GetGameTitleTeamModel gameTitleTeam = await _context.GameTitleTeams
                    .OrderBy(x => x.Id)
                    .Select(x => new GetGameTitleTeamModel
                    {
                        Id = x.Id,
                        GameTitleId = x.GameTitleId,
                        TeamId = x.TeamId,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (gameTitleTeam == null)
                {
                    throw new Exception("GameTitleTeam niet gevonden GetGameTitleTeam 404");
                }

                return gameTitleTeam;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetGameTitleTeamModel> PostGameTitleTeam(PostGameTitleTeamModel postGameTitleTeamModel)
        {
            try
            {
                EntityEntry<GameTitleTeam> result = await _context.GameTitleTeams.AddAsync(new GameTitleTeam
                {
                    GameTitleId = postGameTitleTeamModel.GameTitleId,
                    TeamId = postGameTitleTeamModel.TeamId,
                });

                await _context.SaveChangesAsync();

                return await GetGameTitleTeam(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostGameTitleTeam 400");
            }
        }

        public async Task DeleteGameTitleTeam(Guid id)
        {
            try
            {
                GameTitleTeam gameTitleTeam = await _context.GameTitleTeams
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (gameTitleTeam == null)
                {
                    throw new Exception("GameTitleTeam niet gevonden" + this.GetType().Name + "DeleteGameTitleTeam 404");
                }


                _context.GameTitleTeams.Remove(gameTitleTeam);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "DeleteGameTitleTeam 400");
            }
        }
    }
}