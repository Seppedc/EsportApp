using EsportApp.api.Entities;
using EsportApp.models.UserGameTitles;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EsportApp.api.Repositories
{
    public class UserGameTitleRepository:IUserGameTitleRepository
    {
        private readonly EsportAppContext _context;

        public UserGameTitleRepository(EsportAppContext context)
        {
            _context = context;
        }

        public async Task<GetUserGameTitlesModel> GetUserGameTitles()
        {
            try
            {
                List<GetUserGameTitleModel> userGameTitles = await _context.UserGameTitles
                    .Include(x=> x.GameTitle)
                    .OrderBy(x => x.Id)
                    .Select(x => new GetUserGameTitleModel
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        GameTitleId = x.GameTitleId,
                        Naam = x.GameTitle.Naam,

                    })
                    .AsNoTracking()
                    .ToListAsync();

                if (userGameTitles.Count == 0)
                {
                    userGameTitles = new List<GetUserGameTitleModel>();
                }
                GetUserGameTitlesModel getUserGameTitlesModel = new GetUserGameTitlesModel
                {
                    UserGameTitles = userGameTitles
                };
                return getUserGameTitlesModel;
            }
            catch (Exception e)
            {
                throw new Exception(""+e);
            }
        }

        public async Task<GetUserGameTitleModel> GetUserGameTitle(Guid id)
        {
            try
            {
                GetUserGameTitleModel userGameTitle = await _context.UserGameTitles
                    .Include(x => x.GameTitle)
                    .OrderBy(x => x.Id)
                    .Select(x => new GetUserGameTitleModel
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        GameTitleId = x.GameTitleId,
                        Naam = x.GameTitle.Naam,
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (userGameTitle == null)
                {
                    throw new Exception("UserGameTitle niet gevonden GetUserGameTitle 404");
                }

                return userGameTitle;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<GetUserGameTitleModel> PostUserGameTitle(PostUserGameTitleModel postUserGameTitleModel)
        {
            try
            {
                EntityEntry<UserGameTitle> result = await _context.UserGameTitles.AddAsync(new UserGameTitle
                {
                    UserId = postUserGameTitleModel.UserId,
                    GameTitleId = postUserGameTitleModel.GameTitleId,
                });

                await _context.SaveChangesAsync();

                return await GetUserGameTitle(result.Entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message + "PostUserGameTitle 400");
            }
        }

        public async Task DeleteUserGameTitle(Guid id)
        {
            try
            {
                UserGameTitle userGameTitle = await _context.UserGameTitles
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (userGameTitle == null)
                {
                    throw new Exception("UserGameTitle niet gevonden" + this.GetType().Name + "DeleteUserGameTitle 404");
                }


                _context.UserGameTitles.Remove(userGameTitle);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(""+e.InnerException.Message + this.GetType().Name + "DeleteUserGameTitle 400");
            }
        }
    }
}

