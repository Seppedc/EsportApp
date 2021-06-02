using EsportApp.api.Repositories;
using EsportApp.models.GameTitles;
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
    public class GameTitlesController : ControllerBase
    {
        private readonly IGameTitleRepository _gameTitleRepository;

        public GameTitlesController(IGameTitleRepository gameTitleRepository)
        {
            _gameTitleRepository = gameTitleRepository;
        }

        /// <summary>
        /// Get a list of all GameTitles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/gameTitles
        ///
        /// </remarks>
        /// <returns>GetGameTitelsModel</returns>
        /// <response code="200">Returns the list of GameTitles</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameTitlesModel>> GetGameTitles()
        {
            return await _gameTitleRepository.GetGameTitles();
        }

        /// <summary>
        /// Get details of an GameTitle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/gameTitles/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetGameTitleModel</returns>
        /// <response code="200">Returns the gameTitle</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameTitleModel>> GetGameTitle(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameTitleId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetGameTitle" + "400");
                }

                GetGameTitleModel gameTitle = await _gameTitleRepository.GetGameTitle(gameTitleId);

                return gameTitle;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an gameTitle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/gameTitles
        ///     {
        ///        "Naam": "Name of the gameTitle",
        ///        "Uitgever": "Name of the uitgever"
        ///     }
        ///
        /// </remarks>
        /// <param name="postGameTitleModel"></param>
        /// <returns>A newly created gameTitle</returns>
        /// <response code="201">Returns the newly created gameTitle</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameTitleModel>> PostGameTitle(PostGameTitleModel postGameTitleModel)
        {
            try
            {
                GetGameTitleModel gameTitle = await _gameTitleRepository.PostGameTitle(postGameTitleModel);

                return CreatedAtAction(nameof(GetGameTitle), new { id = gameTitle.Id }, gameTitle);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Updates an gameTitle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/gameTitles/{id}
        ///     {
        ///        "Naam": "Name of the gameTitle",
        ///        "Uitgever": "Name of the uitgever"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <param name="putGameTitleModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The gameTitle could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> PutGameTitle(string id, PutGameTitleModel putGameTitleModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameTitleId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "PutGameTitle" + "400");
                }

                await _gameTitleRepository.PutGameTitle(gameTitleId, putGameTitleModel);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }


        /// <summary>
        /// Deletes an gameTitle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/gameTitles/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The gameTitle could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteGameTitle(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameTitleId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteGameTitle" + "400");
                }

                await _gameTitleRepository.DeleteGameTitle(gameTitleId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
