using EsportApp.api.Repositories;
using EsportApp.models.GameTitleTeams;
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
    public class GameTitleTeamsController : ControllerBase
    {
        private readonly IGameTitleTeamRepository _gameTitleTeamRepository;

        public GameTitleTeamsController(IGameTitleTeamRepository gameTitleTeamRepository)
        {
            _gameTitleTeamRepository = gameTitleTeamRepository;
        }

        /// <summary>
        /// Get a list of all GameTitleTeams.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/gameTitleTeams
        ///
        /// </remarks>
        /// <returns>GetGameTitelTeamsModel</returns>
        /// <response code="200">Returns the list of GameTitleTeams</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameTitleTeamsModel>> GetGameTitleTeams()
        {
            return await _gameTitleTeamRepository.GetGameTitleTeams();
        }

        /// <summary>
        /// Get details of an GameTitleTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/gameTitleTeams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetGameTitleTeamModel</returns>
        /// <response code="200">Returns the gameTitleTeam</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameTitleTeamModel>> GetGameTitleTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameTitleTeamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetGameTitleTeam" + "400");
                }

                GetGameTitleTeamModel gameTitleTeam = await _gameTitleTeamRepository.GetGameTitleTeam(gameTitleTeamId);

                return gameTitleTeam;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an gameTitleTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/gameTitleTeams
        ///     {
        ///        "GameTitleId": "Id of the gameTitle",
        ///        "TeamId": "Id of the Team"
        ///     }
        ///
        /// </remarks>
        /// <param name="postGameTitleTeamModel"></param>
        /// <returns>A newly created gameTitleTeam</returns>
        /// <response code="201">Returns the newly created gameTitleTeam</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameTitleTeamModel>> PostGameTitleTeam(PostGameTitleTeamModel postGameTitleTeamModel)
        {
            try
            {
                GetGameTitleTeamModel gameTitleTeam = await _gameTitleTeamRepository.PostGameTitleTeam(postGameTitleTeamModel);

                return CreatedAtAction(nameof(GetGameTitleTeam), new { id = gameTitleTeam.Id }, gameTitleTeam);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Deletes an gameTitleTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/gameTitleTeams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The gameTitleTeam could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteGameTitleTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameTitleTeamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteGameTitleTeam" + "400");
                }

                await _gameTitleTeamRepository.DeleteGameTitleTeam(gameTitleTeamId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
