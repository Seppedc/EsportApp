using EsportApp.api.Repositories;
using EsportApp.models.UserGames;
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
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserGamesController : ControllerBase
    {
        private readonly IUserGameRepository _userGameRepository;

        public UserGamesController(IUserGameRepository userGameRepository)
        {
            _userGameRepository = userGameRepository;
        }

        /// <summary>
        /// Get a list of all UserGames.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userGames
        ///
        /// </remarks>
        /// <returns>GetUserGamesModel</returns>
        /// <response code="200">Returns the list of UserGames</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserGamesModel>> GetUserGames()
        {
            return await _userGameRepository.GetUserGames();
        }
        /// <summary>
        /// Get a list of all usergames for a user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userGames/{id}/user
        ///
        /// </remarks>
        /// <param name="id"></param>     
        /// <returns>List GetReservatieModel</returns>
        /// <response code="200">Returns the list of reservaties</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}/User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserGamesModel>> GetUserGames(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetUserGame" + "400");
                }

                GetUserGamesModel userGames = await _userGameRepository.GetUserGames(userId);

                return userGames;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }
        /// <summary>
        /// Get details of an UserGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userGames/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetUserGamesModel</returns>
        /// <response code="200">Returns the UserGame</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The gameGame could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserGameModel>> GetUserGame(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userGameId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetUserGame" + "400");
                }

                GetUserGameModel userGame = await _userGameRepository.GetUserGame(userGameId);

                return userGame;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an UserGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/userGame
        ///     {
        ///        "UserId": "Id of the User",
        ///        "GameId": "Id of the Game"
        ///     }
        ///
        /// </remarks>
        /// <param name="postUserGameModel"></param>
        /// <returns>A newly created UserGame</returns>
        /// <response code="201">Returns the newly created UserGame</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserGameModel>> PostUserGame(PostUserGameModel postUserGameModel)
        {
            try
            {
                GetUserGameModel userGame = await _userGameRepository.PostUserGame(postUserGameModel);

                return CreatedAtAction(nameof(GetUserGame), new { id = userGame.Id }, userGame);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Deletes an UserGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/userGames/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The UserGame could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUserGame(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userGameId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteUserGame" + "400");
                }

                await _userGameRepository.DeleteUserGame(userGameId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
