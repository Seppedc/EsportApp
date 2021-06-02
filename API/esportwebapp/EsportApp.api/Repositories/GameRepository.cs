using EsportApp.api.Entities;
using EsportApp.models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EsportApp.models.TeamGames;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EsportApp.api.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly EsportAppContext _context;

        public GameRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetGamesModel> GetGames()
        {
            try
            {
                List<GetGameModel> games = await _context.Games
                    .Include(x => x.TeamGames).ThenInclude(x => x.Team)
                    .Include(x => x.Tornooi)
                    .OrderBy(x => x.Datum)
                    .Select(x => new GetGameModel
                    {
                        Id = x.Id,
                        Score = x.Score,
                        Datum = (DateTime)x.Datum,
                        Status = x.Status,
                        Type = x.Type,
                        Tornooi = x.Tornooi.Naam,
                        TeamGames = x.TeamGames.Select(y=>new GetTeamGameModel
                        {
                            Id=y.Id,
                            TeamId = y.TeamId,
                            GameId = y.GameId,
                        }).ToList(),
                        Teams = x.TeamGames.Select(y => y.Team.Naam).ToList(),
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (games.Count == 0)
                {
                    games = new List<GetGameModel>();
                }
                GetGamesModel getGamesModel = new GetGamesModel
                {
                    Games = games
                };
                return getGamesModel;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetGameModel> GetGame(Guid id)
        {
            try
            {
                GetGameModel game = await _context.Games
                    .Include(x => x.TeamGames)
                    .Include(x => x.Tornooi)
                    .OrderBy(x => x.Datum)
                    .Select(x => new GetGameModel
                    {
                        Id = x.Id,
                        Score = x.Score,
                        Datum = (DateTime)x.Datum,
                        Status = x.Status,
                        Type = x.Type,
                        Tornooi = x.Tornooi.Naam,
                        TeamGames = x.TeamGames.Select(y => new GetTeamGameModel
                        {
                            Id = y.Id,
                            TeamId = y.TeamId,
                            GameId = y.GameId
                        }).ToList(),
                        Teams = x.TeamGames.Select(y => y.Team.Naam).ToList(),

                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return game;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetGameModel> PostGame(PostGameModel postGameModel)
        {
            try
            {
                Tornooi tornooi = await GetTornooi(postGameModel.ToernooiId, "PostGame");

                EntityEntry<Game> result = await _context.Games.AddAsync(new Game
                {
                    Score = postGameModel.Score,
                    Datum = postGameModel.Datum,
                    Status = postGameModel.Status,
                    Type = postGameModel.Type,
                    ToernooiId = tornooi.Id,
                });

                await _context.SaveChangesAsync();

                return await GetGame(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message+"PostGame 400"); 
            }
        }

        public async Task PutGame(Guid id, PutGameModel putGameModel)
        {
            try
            {
                Tornooi tornooi = await GetTornooi(putGameModel.ToernooiId, "PutGame");

                Game game = await _context.Games.FirstOrDefaultAsync(x => x.Id == id);

                if (game == null)
                {
                    throw new Exception("Exemplaar niet gevonden"+ this.GetType().Name+ "PutGame 404");
                }

                game.Score = putGameModel.Score;
                game.Datum = putGameModel.Datum;
                game.Status = putGameModel.Status;
                game.Type = putGameModel.Type;
                game.ToernooiId = tornooi.Id;
                
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message+ this.GetType().Name+ "PutGame 400");
            }
        }

        public async Task DeleteGame(Guid id)
        {
            try
            {
                Game game = await _context.Games
                    .Include(x => x.TeamGames)
                    .Include(x => x.Tornooi)
                    .OrderBy(x => x.Datum)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (game == null)
                {
                    throw new Exception("Exemplaar niet gevonden"+ this.GetType().Name+ "DeleteGame 404");
                }


                //if (_context.Games.Where(x => x.ToernooiId == game.ToernooiId).Count() == 1)
                //{
                //    _context.TornooiTeams.RemoveRange(game.Tornooi.TornooiTeams);
                //    _context.Tornooien.Remove(game.Tornooi);
                //}
                

                List<TeamGame> teamGames = await _context.TeamGames.Where(x => x.GameId == game.Id).ToListAsync();
                _context.TeamGames.RemoveRange(teamGames);
                List<UserGame> userGames = await _context.UserGames.Where(x => x.GameId == game.Id).ToListAsync();
                _context.UserGames.RemoveRange(userGames);
               
                _context.Games.Remove(game);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message+ this.GetType().Name+ "DeleteGame 400");
            }
        }

        private async Task<Game> GetGame(Guid id, string sourceMethod)
        {
            Game game = await _context.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (game == null)
            {
                throw new Exception("Game niet gevonden " + this.GetType().Name + sourceMethod + "404");

            }

            return game;
        } 
        private async Task<Tornooi> GetTornooi(Guid id, string sourceMethod)
        {
            Tornooi tornooi = await _context.Tornooien.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (tornooi == null)
            {
                throw new Exception("Tornooi niet gevonden"+ this.GetType().Name+ sourceMethod+ "404");

            }
            return tornooi;
        }
    }
}
