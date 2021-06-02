using EsportApp.api.Entities;
using EsportApp.api.Helpers;
using EsportApp.models.RefreshTokens;
using EsportApp.models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EsportAppContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly ClaimsPrincipal _user;

        public UserRepository(
            EsportAppContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;
            _user = _httpContextAccessor.HttpContext.User;
        }

        public async Task<GetUsersModel> GetUsers()
        {
            List<GetUserModel> users = await _context.Users
                .Include(x => x.UserRoles)
                .OrderBy(x => x.Voornaam)
                .Select(x => new GetUserModel
                {
                    Id = x.Id,
                    Voornaam = x.Voornaam,
                    Familienaam = x.Familienaam,
                    Email = x.Email,
                    Roles = x.UserRoles.Select(x => x.Role.Name).ToList()
                })
                .AsNoTracking()
                .ToListAsync();

          
            GetUsersModel getUsersModel = new GetUsersModel
            {
                Users = users
            };

            return getUsersModel;
        }

        public async Task<GetUserModel> GetUser(Guid id)
        {
            if (_user.Claims.Where(x => x.Type.Contains("role")).Count() == 1 &&
                _user.IsInRole("User") &&
                _user.Identity.Name != id.ToString())
            {
                throw new Exception("Geen toegang tot de details van deze gebruiker"+ this.GetType().Name+ "GetUser"+ "403");
            }

            GetUserModel user = await _context.Users
                .Include(x => x.UserRoles)
                .Include(x => x.RefreshTokens)
                .Select(x => new GetUserModel
                {
                    Id = x.Id,
                    Voornaam = x.Voornaam,
                    Familienaam = x.Familienaam,
                    Email = x.Email,
                    Roles = x.UserRoles.Select(x => x.Role.Name).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("Gebruiker niet gevonden"+ this.GetType().Name+ "GetUser"+ "404");
            }

            return user;
        }

        public async Task<GetUserModel> PostUser(PostUserModel postUserModel)
        {

            User user = new User
            {
                Voornaam = postUserModel.Voornaam,
                Familienaam = postUserModel.Familienaam,
                Email = postUserModel.Email,
                Punten = 0,
            };

            IdentityResult userResult = await _userManager.CreateAsync(user, postUserModel.Password);

            if (!userResult.Succeeded)
            {
                string description = userResult.Errors.First().Description;

                if (description.Contains("is already taken"))
                {
                    description = "Dit e-mailadres is reeds geregistreerd";
                }

                throw new Exception(""+description+ this.GetType().Name+ "PostUser"+ "400");
            }

            try
            {
                if (postUserModel.Roles == null)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    await _userManager.AddToRolesAsync(user, postUserModel.Roles);
                }
            }
            catch (Exception e)
            {
                await _userManager.DeleteAsync(user);
                throw new Exception(""+e.Message+ this.GetType().Name+ "PostUser"+ "400");
            }

            return await GetUser(user.Id);
        }

        public async Task PutUser(Guid id, PutUserModel putUserModel)
        {
            if (_user.Claims.Where(x => x.Type.Contains("role")).Count() == 1 &&
                _user.IsInRole("User") &&
                _user.Identity.Name != id.ToString())
            {
                throw new Exception("Geen toegang om de gegevens van deze gebruiker te wijzigen"+ this.GetType().Name+ "PutUser"+ "403");
            }

            try
            {
                if (putUserModel.Roles == null)
                {
                    throw new Exception("Gebruiker moet minstens één rol hebben"+ this.GetType().Name+"PutUser"+ "400");
                }

                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    throw new Exception("Gebruiker niet gevonden"+ this.GetType().Name+ "PutUser"+ "404");
                }

                user.Voornaam = putUserModel.Voornaam;
                user.Familienaam = putUserModel.Familienaam;
                user.Email = putUserModel.Email;
                user.Punten = putUserModel.Punten;


                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    string description = result.Errors.First().Description;

                    if (description.Contains("is already taken"))
                    {
                        description = "Dit e-mailadres is reeds geregistreerd";
                    }

                    throw new Exception(""+description+ this.GetType().Name+ "PutUser"+ "400");
                }
                else
                {
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                    await _userManager.AddToRolesAsync(user, putUserModel.Roles);
                }
            }
            catch (Exception e)
            {
                if (e.GetType().Name.Equals("InvalidOperationException"))
                {
                    throw new Exception(""+e.Message+ this.GetType().Name+ "PutUser"+ "400");
                }

                throw new Exception(""+e.InnerException.Message+ this.GetType().Name+ "PutUser"+ "400");
            }
        }

        public async Task PatchUser(Guid id, PatchUserModel patchUserModel)
        {
            if (_user.Claims.Where(x => x.Type.Contains("role")).Count() == 1 &&
                _user.IsInRole("User") &&
                _user.Identity.Name != id.ToString())
            {
                throw new Exception("Geen toelating om deze gebruiker zijn wachtwoord te wijzigen"+ this.GetType().Name+ "PatchUser"+ "403");
            }

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("Gebruiker niet gevonden"+ this.GetType().Name+ "PatchUser"+ "404");
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, patchUserModel.CurrentPassword, patchUserModel.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception(""+result.Errors.First().Description+ this.GetType().Name+ "PatchUser"+ "400");
            }
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                User user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    throw new Exception("Gebruiker niet gevonden"+ this.GetType().Name+ "DeleteUser"+ "404");
                }

                List<UserGame> userGames = await _context.UserGames.Where(x => x.UserId == user.Id).ToListAsync();
                _context.UserGames.RemoveRange(userGames);
                List<UserTeam> UserTeam = await _context.UserTeams.Where(x => x.UserId == user.Id).ToListAsync();
                _context.UserTeams.RemoveRange(UserTeam);
                List<UserGameTitle> userGameTitles = await _context.UserGameTitles.Where(x => x.UserId == user.Id).ToListAsync();
                _context.UserGameTitles.RemoveRange(userGameTitles);

                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message+ this.GetType().Name+ "DeleteUser"+ "400");
            }
        }

        public async Task<List<GetRefreshTokenModel>> GetUserRefreshTokens(Guid id)
        {
            List<GetRefreshTokenModel> refreshTokens = await _context.RefreshTokens
                .Where(x => x.UserId == id)
                .Select(x => new GetRefreshTokenModel
                {
                    Id = x.Id,
                    Token = x.Token,
                    Expires = x.Expires,
                    IsExpired = x.IsExpired,
                    Created = x.Created,
                    CreatedByIp = x.CreatedByIp,
                    Revoked = x.Revoked,
                    RevokedByIp = x.RevokedByIp,
                    ReplacedByToken = x.ReplacedByToken,
                    IsActive = x.IsActive
                })
                .AsNoTracking()
                .ToListAsync();

            if (refreshTokens.Count == 0)
            {
                throw new Exception("Geen refresh tokens gevonden"+ this.GetType().Name+ "GetUserRefreshTokens"+ "404");
            }

            return refreshTokens;
        }

        // JWT Methods
        // ===========

        public async Task<PostAuthenticateResponseModel> Authenticate(PostAuthenticateRequestModel postAuthenticateRequestModel, string ipAddress)
        {
            User user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.UserName == postAuthenticateRequestModel.UserName);

            if (user == null)
            {
                throw new Exception("Ongeldig e-mailadres"+ this.GetType().Name+ "Authenticate"+ "401");
            }

            // Verify password when user was found by UserName
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, postAuthenticateRequestModel.Password, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new Exception("Ongeldig wachtwoord"+ this.GetType().Name+ "Authenticate"+ "401");
            }

            // Authentication was successful so generate JWT and refresh tokens
            string jwtToken = await GenerateJwtToken(user);
            RefreshToken refreshToken = GenerateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);

            return new PostAuthenticateResponseModel
            {
                Id = user.Id,
                Voornaam = user.Voornaam,
                Familienaam = user.Familienaam,
                UserName = user.UserName,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        public async Task<PostAuthenticateResponseModel> RefreshToken(string token, string ipAddress)
        {
            User user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new Exception("Geen gebruiker gevonden met dit token"+ this.GetType().Name+ "RefreshToken"+ "401");
            }

            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // Refresh token is no longer active
            if (!refreshToken.IsActive)
            {
                throw new Exception("Refresh token is vervallen"+ this.GetType().Name+ "RefreshToken"+ "401");
            };

            // Replace old refresh token with a new one
            RefreshToken newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            // Generate new jwt
            string jwtToken = await GenerateJwtToken(user);

            user.RefreshTokens.Add(newRefreshToken);

            await _userManager.UpdateAsync(user);

            return new PostAuthenticateResponseModel
            {
                Id = user.Id,
                Voornaam = user.Voornaam,
                Familienaam = user.Familienaam,
                UserName = user.UserName,
                JwtToken = jwtToken,
                RefreshToken = newRefreshToken.Token,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            User user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new Exception("Geen gebruiker gevonden met dit token"+ this.GetType().Name+ "RefreshToken"+ "401");
            }

            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // Refresh token is no longer active
            if (!refreshToken.IsActive)
            {
                throw new Exception("Refresh token is vervallen"+ this.GetType().Name+ "RefreshToken"+ "401");
            };

            // Revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _userManager.UpdateAsync(user);
        }

        // JWT helper methods
        // ==================

        private async Task<string> GenerateJwtToken(User user)
        {
            var roleNames = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            claims.Add(new Claim("Voornaam", user.Voornaam));
            claims.Add(new Claim("Familienaam", user.Familienaam));
            
            foreach (string roleName in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "EsportApp web API",
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddSeconds(40), //TOKEN JWT
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            // The refresh token expires time must be the same as the refresh token 
            // cookie expires time set in the SetTokenCookie method in UsersController
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddMinutes(5), //TOKEN REFRESH
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
    }
}
