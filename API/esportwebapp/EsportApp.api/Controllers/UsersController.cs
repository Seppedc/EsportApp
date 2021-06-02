using EsportApp.api.Repositories;
using EsportApp.models.RefreshTokens;
using EsportApp.models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/users
        ///
        /// </remarks>
        /// <returns>GetUsersModel</returns>
        /// <response code="200">Returns the list of users</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUsersModel>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        /// <summary>
        /// Get details of an user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/users/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>     
        /// <returns>An GetUserModel</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">The user could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - requested user id does not match with authenticated user id</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserModel>> GetUser(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "GetUser"+ "400");
                }

                GetUserModel user = await _userRepository.GetUser(userId);

                return user;
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Creates an user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users
        ///     {
        ///        "Voornaam": "Seppe",
        ///        "Familienaam": "De Craene",
        ///        "Email": "seppe@decraene.com",
        ///        "Password": "Azerty123",
        ///        "Roles": [
        ///            "User"
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="postUserModel"></param>
        /// <returns>A newly created user</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If something went wrong while saving into the database</response>   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserModel>> PostUser(PostUserModel postUserModel)
        {
            try
            {
                GetUserModel user = await _userRepository.PostUser(postUserModel);

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Updates an user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/{id}
        ///     {
        ///         "Voornaam": "Seppe",
        ///        "Familienaam": "De Craene",
        ///        "Email": "seppe@decraene.com",
        ///        "Roles": [
        ///            "User"
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>     
        /// <param name="putUserModel"></param>    
        /// <response code="204">Returns no content</response>
        /// <response code="404">The user could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - requested user id does not match with authenticated user id</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<IActionResult> PutUser(string id, PutUserModel putUserModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "PutUser"+ "400");
                }

                await _userRepository.PutUser(userId, putUserModel);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Updates an user password.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/users/{id}
        ///     {
        ///        "CurrentPassword": "Azerty123",
        ///        "NewPassword": "_Azerty123!"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>     
        /// <param name="patchUserModel"></param>    
        /// <response code="204">Returns no content</response>
        /// <response code="404">The user could not be found</response> 
        /// <response code="400">The id is not a valid Guid or the current password does not match or the new password is not conform the password rules</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - requested user id does not match with authenticated user id</response> 
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]

        public async Task<IActionResult> PatchUser(string id, PatchUserModel patchUserModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "PatchUser"+ "400");
                }

                await _userRepository.PatchUser(userId, patchUserModel);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Deletes an user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/users/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>     
        /// <response code="204">Returns no content</response>
        /// <response code="404">The user could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "DeleteUser"+ "400");
                }

                await _userRepository.DeleteUser(userId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        // JWT Action Methods
        // ==================

        /// <summary>
        /// Authenticates an user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users/authenticate
        ///     {
        ///        "UserName": "seppe@decraene.com",
        ///        "Password": "_Azerty123"
        ///     }
        ///
        /// </remarks>
        /// <param name="postAuthenticateRequestModel"></param>
        /// <returns>Details of authenticated user, an JWT token and a refresh token</returns>
        /// <response code="200">Returns the authenticated user with tokens</response>
        /// <response code="401">Incorrect credentials</response>   
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<PostAuthenticateResponseModel>> Authenticate(PostAuthenticateRequestModel postAuthenticateRequestModel)
        {
            try
            {
                PostAuthenticateResponseModel postAuthenticateResponseModel = await _userRepository.Authenticate(postAuthenticateRequestModel, IpAddress());

                SetTokenCookie(postAuthenticateResponseModel.RefreshToken);

                return postAuthenticateResponseModel;
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }

        /// <summary>
        /// Renew tokens.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users/refresh-token
        ///
        /// </remarks>
        /// <returns>Details of authenticated user, a new JWT token and a new refresh token</returns>
        /// <response code="200">Returns the authenticated user with new tokens</response>
        /// <response code="401">Invalid refresh token</response>   
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<PostAuthenticateResponseModel>> RefreshToken()
        {
            try
            {
                string refreshToken = Request.Cookies["Mediatheek.RefreshToken"];

                PostAuthenticateResponseModel postAuthenticateResponseModel = await _userRepository.RefreshToken(refreshToken, IpAddress());

                SetTokenCookie(postAuthenticateResponseModel.RefreshToken);

                return postAuthenticateResponseModel;
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }

        /// <summary>
        /// Revoke token.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users/revoke-token
        ///     {
        ///        "Token": "Some token"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Disables a refresh token</response>
        /// <response code="400">No token present in body or cookie</response>   
        /// <response code="401">No user found with this token or refresh token is no longer active</response>   
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> RevokeToken(PostRevokeTokenRequestModel postRevokeTokenRequestModel)
        {
            try
            {
                // Accept token from request body or cookie
                string token = postRevokeTokenRequestModel.Token ?? Request.Cookies["Mediatheek.RefreshToken"];

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Refresh token is verplicht"+ this.GetType().Name+ "RevokeToken"+ "400");
                }

                await _userRepository.RevokeToken(token, IpAddress());

                return Ok();
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }

        /// <summary>
        /// Get a list of all refresh tokens of a user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/users/{id}/refresh-tokens
        ///
        /// </remarks>
        /// <returns>List of GetRefreshTokenModel</returns>
        /// <response code="200">Returns the list of refresh tokens</response>
        /// <response code="404">No refresh tokens were found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}/refresh-tokens")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetRefreshTokenModel>>> GetUserRefreshTokens(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "GetUserRefreshTokens"+ "400");
                }

                List<GetRefreshTokenModel> refreshTokens = await _userRepository.GetUserRefreshTokens(userId);

                return refreshTokens;
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // JWT helper methods
        // ==================

        // The refresh token is being sent in an HTTP only cookie and as a header
        private void SetTokenCookie(string token)
        {

            // The cookie expires time must be the same as the refresh token 
            // expires time set in the GenerateRefreshToken method in UserRepository
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(5), //TOKEN REFRESH
                IsEssential = true
            };

            Response.Cookies.Append("EsportApp.RefreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}
