﻿using EsportApp.api.Entities;
using EsportApp.models.UserGames;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class UserGameRepository : IUserGameRepository
    {
        private readonly EsportAppContext _context;

        public UserGameRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetUserGamesModel> GetUserGames()
        {
            try
            {
                List<GetUserGameModel> userGames = await _context.UserGames
                    .OrderBy(x => x.Id)
                    .Select(x => new GetUserGameModel
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        GameId = x.GameId,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (userGames.Count == 0)
                {
                    userGames = new List<GetUserGameModel>();
                }
                GetUserGamesModel getUserGamesModel = new GetUserGamesModel
                {
                    UserGames = userGames
                };
                return getUserGamesModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetUserGameModel> GetUserGame(Guid id)
        {
            try
            {
                GetUserGameModel userGame = await _context.UserGames
                    .OrderBy(x => x.Id)
                    .Select(x => new GetUserGameModel
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        GameId = x.GameId,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (userGame == null)
                {
                    throw new Exception("UserGame niet gevonden GetUserGame 404");
                }

                return userGame;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetUserGameModel> PostUserGame(PostUserGameModel postUserGameModel)
        {
            try
            {
                EntityEntry<UserGame> result = await _context.UserGames.AddAsync(new UserGame
                {
                    UserId = postUserGameModel.UserId,
                    GameId = postUserGameModel.GameId,
                });

                await _context.SaveChangesAsync();

                return await GetUserGame(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostUserGame 400");
            }
        }

        public async Task DeleteUserGame(Guid id)
        {
            try
            {
                UserGame userGame = await _context.UserGames
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (userGame == null)
                {
                    throw new Exception("UserGame niet gevonden" + this.GetType().Name + "DeleteUserGame 404");
                }


                _context.UserGames.Remove(userGame);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + this.GetType().Name + "DeleteUserGame 400");
            }
        }
    }
}