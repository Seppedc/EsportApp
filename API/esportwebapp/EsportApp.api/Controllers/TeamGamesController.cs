using EsportApp.api.Repositories;
using EsportApp.models.TeamGames;
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
    public class TeamGamesController : ControllerBase
    {
        private readonly ITeamGameRepository _teamGameRepository;

        public TeamGamesController(ITeamGameRepository teamGameRepository)
        {
            _teamGameRepository = teamGameRepository;
        }

        /// <summary>
        /// Get a list of all TeamsGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/teamGames
        ///
        /// </remarks>
        /// <returns>GetTeamGamesModel</returns>
        /// <response code="200">Returns the list of TeamGames</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTeamGamesModel>> GetTeamGames()
        {
            return await _teamGameRepository.GetTeamGames();
        }

        /// <summary>
        /// Get details of an TeamGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/teamGames/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetTeamGamesModel</returns>
        /// <response code="200">Returns the TeamGame</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The gameGame could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTeamGameModel>> GetTeamGame(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid teamGameId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetTeamGame" + "400");
                }

                GetTeamGameModel teamGame = await _teamGameRepository.GetTeamGame(teamGameId);

                return teamGame;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an TeamGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/teamGame
        ///     {
        ///        "TeamId": "Id of the Team",
        ///        "GameId": "Id of the Game"
        ///     }
        ///
        /// </remarks>
        /// <param name="postTeamGameModel"></param>
        /// <returns>A newly created TeamGame</returns>
        /// <response code="201">Returns the newly created TeamGame</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTeamGameModel>> PostTeamGame(PostTeamGameModel postTeamGameModel)
        {
            try
            {
                GetTeamGameModel teamGame = await _teamGameRepository.PostTeamGame(postTeamGameModel);

                return CreatedAtAction(nameof(GetTeamGame), new { id = teamGame.Id }, teamGame);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Deletes an TeamGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/teamGames/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The TeamGame could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTeamGame(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid teamGameId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteTeamGame" + "400");
                }

                await _teamGameRepository.DeleteTeamGame(teamGameId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
