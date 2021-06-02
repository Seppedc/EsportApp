using EsportApp.api.Repositories;
using EsportApp.models.Teams;
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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        /// <summary>
        /// Get a list of all Teams.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/teams
        ///
        /// </remarks>
        /// <returns>GetTeamsModel</returns>
        /// <response code="200">Returns the list of Teams</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTeamsModel>> GetTeams()
        {
            return await _teamRepository.GetTeams();
        }

        /// <summary>
        /// Get details of an Team.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/teams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetTeamsModel</returns>
        /// <response code="200">Returns the Team</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTeamModel>> GetTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid teamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetTeam" + "400");
                }

                GetTeamModel team = await _teamRepository.GetTeam(teamId);

                return team;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an Team.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/team
        ///     {
        ///        "Naam": "Name of the Team",
        ///     }
        ///
        /// </remarks>
        /// <param name="postTeamModel"></param>
        /// <returns>A newly created Team</returns>
        /// <response code="201">Returns the newly created Team</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTeamModel>> PostTeam(PostTeamModel postTeamModel)
        {
            try
            {
                GetTeamModel team = await _teamRepository.PostTeam(postTeamModel);

                return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Updates an Team.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/teams/{id}
        ///     {
        ///        "Naam": "Name of the team",
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <param name="putTeamModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The Team could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> PutTeam(string id, PutTeamModel putTeamModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid teamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "PutTeam" + "400");
                }

                await _teamRepository.PutTeam(teamId, putTeamModel);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }


        /// <summary>
        /// Deletes an Team.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/teams/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The Team could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTeam(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid teamId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteTeam" + "400");
                }

                await _teamRepository.DeleteTeam(teamId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
