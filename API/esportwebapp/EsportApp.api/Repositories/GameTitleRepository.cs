using EsportApp.api.Entities;
using EsportApp.models.GameTitles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class GameTitleRepository : IGameTitleRepository
    {
        private readonly EsportAppContext _context;

        public GameTitleRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetGameTitlesModel> GetGameTitles()
        {
            try
            {
                List<GetGameTitleModel> gameTitles = await _context.GameTitles
                    .OrderBy(x => x.Naam)
                    .Select(x => new GetGameTitleModel
                    {
                        Id = x.Id,
                        Naam = x.Naam,
                        Uitgever = x.Uitgever,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (gameTitles.Count == 0)
                {
                    gameTitles = new List<GetGameTitleModel>();
                }
                GetGameTitlesModel getGameTitlesModel = new GetGameTitlesModel
                {
                    GameTitles = gameTitles
                };
                return getGameTitlesModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetGameTitleModel> GetGameTitle(Guid id)
        {
            try
            {
                GetGameTitleModel gameTitle = await _context.GameTitles
                    .OrderBy(x => x.Naam)
                    .Select(x => new GetGameTitleModel
                    {
                        Id = x.Id,
                        Naam = x.Naam,
                        Uitgever = x.Uitgever,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (gameTitle == null)
                {
                    throw new Exception("GameTitle niet gevonden GetGameTitle 404");
                }

                return gameTitle;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetGameTitleModel> PostGameTitle(PostGameTitleModel postGameTitleModel)
        {
            try
            {
                EntityEntry<GameTitle> result = await _context.GameTitles.AddAsync(new GameTitle
                {
                    Naam = postGameTitleModel.Naam,
                    Uitgever = postGameTitleModel.Uitgever,
                });

                await _context.SaveChangesAsync();

                return await GetGameTitle(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostGameTitle 400");
            }
        }

        public async Task PutGameTitle(Guid id, PutGameTitleModel putGameTitleModel)
        {
            try
            {
                GameTitle gameTitle = await _context.GameTitles.FirstOrDefaultAsync(x => x.Id == id);

                if (gameTitle == null)
                {
                    throw new Exception("GameTitle niet gevonden" + this.GetType().Name + "PutgameTitle 404");
                }
                gameTitle.Naam = putGameTitleModel.Naam;
                gameTitle.Uitgever = putGameTitleModel.Uitgever;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "PutGameTitle 400");
            }
        }

        public async Task DeleteGameTitle(Guid id)
        {
            try
            {
                GameTitle gameTitle = await _context.GameTitles
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (gameTitle == null)
                {
                    throw new Exception("GameTitle niet gevonden" + this.GetType().Name + "DeleteGameTitle 404");
                }

                List<UserGameTitle> userGameTitles = await _context.UserGameTitles.Where(x => x.GameTitleId == gameTitle.Id).ToListAsync();
                _context.UserGameTitles.RemoveRange(userGameTitles);
                List<GameTitleTeam> gameTitleTeams = await _context.GameTitleTeams.Where(x => x.GameTitleId == gameTitle.Id).ToListAsync();
                _context.GameTitleTeams.RemoveRange(gameTitleTeams);

                List<Tornooi> tornooien = await _context.Tornooien.Where(x => x.GameTitleId == gameTitle.Id).ToListAsync();
                _context.Tornooien.RemoveRange(tornooien);

                _context.GameTitles.Remove(gameTitle);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "DeleteGameTitle 400");
            }
        }
    }
}

