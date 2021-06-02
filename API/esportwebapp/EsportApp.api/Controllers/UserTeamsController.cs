using EsportApp.api.Repositories;
using EsportApp.models.UserTeams;
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
    public class UserTeamsController : ControllerBase
    {
        private readonly IUserTeamRepository _userTeamRepository;

        public UserTeamsController(IUserTeamRepository userTeamRepository)
        {
            _userTeamRepository = userTeamRepository;
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
        public async Task<ActionResult<GetUserTeamsModel>> GetUserTeams()
        {
            return await _userTeamRepository.GetUserTeams();
        }

        /// <summary>
        /// Get details of an UserTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userTeams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetUserTeamsModel</returns>
        /// <response code="200">Returns the UserTeam</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The gameGame could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserTeamModel>> GetUserTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userTeamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetUserTeam" + "400");
                }

                GetUserTeamModel userTeam = await _userTeamRepository.GetUserTeam(userTeamId);

                return userTeam;
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
        public async Task<ActionResult<GetUserTeamModel>> PostUserTeam(PostUserTeamModel postUserTeamModel)
        {
            try
            {
                GetUserTeamModel userTeam = await _userTeamRepository.PostUserTeam(postUserTeamModel);

                return CreatedAtAction(nameof(GetUserTeam), new { id = userTeam.Id }, userTeam);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Deletes an UserTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/userTeams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The UserTeam could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUserTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userTeamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteUserTeam" + "400");
                }

                await _userTeamRepository.DeleteUserTeam(userTeamId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
