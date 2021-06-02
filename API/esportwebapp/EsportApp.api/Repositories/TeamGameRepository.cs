using EsportApp.api.Entities;
using EsportApp.models.TeamGames;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class TeamGameRepository : ITeamGameRepository
    {
        private readonly EsportAppContext _context;

        public TeamGameRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetTeamGamesModel> GetTeamGames()
        {
            try
            {
                List<GetTeamGameModel> teamGames = await _context.TeamGames
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTeamGameModel
                    {
                        Id = x.Id,
                        TeamId = x.TeamId,
                        GameId = x.GameId,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (teamGames.Count == 0)
                {
                    teamGames = new List<GetTeamGameModel>();
                }
                GetTeamGamesModel getTeamGamesModel = new GetTeamGamesModel
                {
                    TeamGames = teamGames
                };
                return getTeamGamesModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetTeamGameModel> GetTeamGame(Guid id)
        {
            try
            {
                GetTeamGameModel teamGame = await _context.TeamGames
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTeamGameModel
                    {
                        Id = x.Id,
                        TeamId = x.TeamId,
                        GameId = x.GameId,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (teamGame == null)
                {
                    throw new Exception("TeamGame niet gevonden GetTeamGame 404");
                }

                return teamGame;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetTeamGameModel> PostTeamGame(PostTeamGameModel postTeamGameModel)
        {
            try
            {
                EntityEntry<TeamGame> result = await _context.TeamGames.AddAsync(new TeamGame
                {
                    TeamId = postTeamGameModel.TeamId,
                    GameId = postTeamGameModel.GameId,
                });

                await _context.SaveChangesAsync();

                return await GetTeamGame(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostTeamGame 400");
            }
        }

        public async Task DeleteTeamGame(Guid id)
        {
            try
            {
                TeamGame teamGame = await _context.TeamGames
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (teamGame == null)
                {
                    throw new Exception("TeamGame niet gevonden" + this.GetType().Name + "DeleteTeamGame 404");
                }


                _context.TeamGames.Remove(teamGame);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "DeleteTeamGame 400");
            }
        }
    }
}
