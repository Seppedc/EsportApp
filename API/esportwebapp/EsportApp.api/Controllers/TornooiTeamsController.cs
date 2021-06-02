using EsportApp.api.Repositories;
using EsportApp.models.TornooiTeams;
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
    public class TornooiTeamsController : ControllerBase
    {
        private readonly ITornooiTeamRepository _tornooiTeamRepository;

        public TornooiTeamsController(ITornooiTeamRepository tornooiTeamRepository)
        {
            _tornooiTeamRepository = tornooiTeamRepository;
        }

        /// <summary>
        /// Get a list of all TornooiTeams.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tornooiTeams
        ///
        /// </remarks>
        /// <returns>GetTornooiTeamsModel</returns>
        /// <response code="200">Returns the list of TornooiTeams</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooiTeamsModel>> GetTornooiTeams()
        {
            return await _tornooiTeamRepository.GetTornooiTeams();
        }

        /// <summary>
        /// Get details of an TornooiTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tornooiTeams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetTornooiTeamModel</returns>
        /// <response code="200">Returns the TornooiTeam</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooiTeamModel>> GetTornooiTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid tornooiTeamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetTornooiTeam" + "400");
                }

                GetTornooiTeamModel tornooiTeam = await _tornooiTeamRepository.GetTornooiTeam(tornooiTeamId);

                return tornooiTeam;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an TornooiTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/tornooiTeams
        ///     {
        ///        "TornooiId": "id of the Tornooi",
        ///        "TeamId": "id of the Team",
        ///     }
        ///
        /// </remarks>
        /// <param name="postTornooiTeamModel"></param>
        /// <returns>A newly created TornooiTeam</returns>
        /// <response code="201">Returns the newly created TornooiTeam</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooiTeamModel>> PostTornooiTeam(PostTornooiTeamModel postTornooiTeamModel)
        {
            try
            {
                GetTornooiTeamModel tornooiTeam = await _tornooiTeamRepository.PostTornooiTeam(postTornooiTeamModel);

                return CreatedAtAction(nameof(GetTornooiTeam), new { id = tornooiTeam.Id }, tornooiTeam);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }


        /// <summary>
        /// Deletes an TornooiTeam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/tornooiTeams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The TornooiTeam could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTornooiTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid tornooiTeamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteTornooiTeam" + "400");
                }

                await _tornooiTeamRepository.DeleteTornooiTeam(tornooiTeamId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
