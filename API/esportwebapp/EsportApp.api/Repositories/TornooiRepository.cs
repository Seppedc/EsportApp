using EsportApp.api.Entities;
using EsportApp.models.Tornooien;
using EsportApp.models.TornooiTeams;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class TornooiRepository : ITornooiRepository
    {
        private readonly EsportAppContext _context;

        public TornooiRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetTornooienModel> GetTornooien()
        {
            try
            {
                List<GetTornooiModel> tornooien = await _context.Tornooien
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTornooiModel
                    {
                        Id = x.Id,
                        Naam = x.Naam,
                        Organisator = x.Organisator,
                        Beschrijving = x.Beschrijving,
                        Type = x.Type,
                        GameTitleId = x.GameTitleId,
                       
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (tornooien.Count == 0)
                {
                    tornooien = new List<GetTornooiModel>();
                }
                GetTornooienModel getTornooienModel = new GetTornooienModel
                {
                    Tornooien = tornooien
                };
                return getTornooienModel;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetTornooiModel> GetTornooi(Guid id)
        {
            try
            {
                GetTornooiModel tornooi = await _context.Tornooien
                    .OrderBy(x => x.Id)
                    .Select(x => new GetTornooiModel
                    {
                        Id = x.Id,
                        Naam = x.Naam,
                        Organisator = x.Organisator,
                        Beschrijving = x.Beschrijving,
                        Type = x.Type,
                        GameTitleId = x.GameTitleId,

                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tornooi == null)
                {
                    throw new Exception("tornooi niet gevonden GetTornooi 404");
                }

                return tornooi;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetTornooiModel> PostTornooi(PostTornooiModel postTornooiModel)
        {
            try
            {
                GameTitle gameTitle = await GetGameTitle(postTornooiModel.GameTitleId, "PostTornooi");

                EntityEntry<Tornooi> result = await _context.Tornooien.AddAsync(new Tornooi
                {
                    Naam = postTornooiModel.Naam,
                    Organisator = postTornooiModel.Organisator,
                    Beschrijving = postTornooiModel.Beschrijving,
                    Type = postTornooiModel.Type,
                    GameTitleId = gameTitle.Id,
                });

                await _context.SaveChangesAsync();

                return await GetTornooi(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostTornooi 400");
            }
        }

        public async Task PutTornooi(Guid id, PutTornooiModel putTornooiModel)
        {
            try
            {
                GameTitle gameTitle = await GetGameTitle(putTornooiModel.GameTitleId, "PutTornooi");

                Tornooi tornooi = await _context.Tornooien.FirstOrDefaultAsync(x => x.Id == id);

                if (tornooi == null)
                {
                    throw new Exception("tornooi niet gevonden" + this.GetType().Name + "PutTornooi 404");
                }

                tornooi.Naam = putTornooiModel.Naam;
                tornooi.Organisator = putTornooiModel.Organisator;
                tornooi.Beschrijving = putTornooiModel.Beschrijving;
                tornooi.Type = putTornooiModel.Type;
                tornooi.GameTitleId = gameTitle.Id;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "PutTornooi 400");
            }
        }

        public async Task DeleteTornooi(Guid id)
        {
            try
            {
                Tornooi tornooi = await _context.Tornooien
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tornooi == null)
                {
                    throw new Exception("Tornooi niet gevonden" + this.GetType().Name + "DeleteTornooi 404");
                }


                //if (_context.Games.Where(x => x.ToernooiId == game.ToernooiId).Count() == 1)
                //{
                //    _context.TornooiTeams.RemoveRange(game.Tornooi.TornooiTeams);
                //    _context.Tornooien.Remove(game.Tornooi);
                //}


                List<TornooiTeam> tornooiTeams = await _context.TornooiTeams.Where(x => x.TeamId == tornooi.Id).ToListAsync();
                _context.TornooiTeams.RemoveRange(tornooiTeams);
                List<Game> games = await _context.Games.Where(x => x.ToernooiId == tornooi.Id).ToListAsync();
                _context.Games.RemoveRange(games);

                _context.Tornooien.Remove(tornooi);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "DeleteTornooi 400");
            }
        }

        private async Task<GameTitle> GetGameTitle(Guid id, string sourceMethod)
        {
            GameTitle gameTitle = await _context.GameTitles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (gameTitle == null)
            {
                throw new Exception("gameTitle niet gevonden  404");

            }

            return gameTitle;
        }
    }
}
