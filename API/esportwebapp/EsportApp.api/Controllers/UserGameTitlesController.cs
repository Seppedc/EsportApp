using EsportApp.api.Repositories;
using EsportApp.models.UserGameTitles;
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
    public class UserGameTitlesController : ControllerBase
    {
        private readonly IUserGameTitleRepository _userGameTitleRepository;

        public UserGameTitlesController(IUserGameTitleRepository userGameTitleRepository)
        {
            _userGameTitleRepository = userGameTitleRepository;
        }

        /// <summary>
        /// Get a list of all UserGamesTitle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userGameTitles
        ///
        /// </remarks>
        /// <returns>GetUserGameTitlesModel</returns>
        /// <response code="200">Returns the list of UserGameTitles</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserGameTitlesModel>> GetUserGameTitles()
        {
            return await _userGameTitleRepository.GetUserGameTitles();
        }

        /// <summary>
        /// Get details of an UserGameTitle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userGameTitles/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetUserGameTitlesModel</returns>
        /// <response code="200">Returns the UserGameTitle</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The gameGame could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserGameTitleModel>> GetUserGameTitle(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userGameTitleId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetUserGameTitle" + "400");
                }

                GetUserGameTitleModel userGameTitle = await _userGameTitleRepository.GetUserGameTitle(userGameTitleId);

                return userGameTitle;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an UserGameTitle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/userGameTitle
        ///     {
        ///        "UserId": "Id of the User",
        ///        "GameTitleId": "Id of the GameTitle"
        ///     }
        ///
        /// </remarks>
        /// <param name="postUserGameModel"></param>
        /// <returns>A newly created UserGameTitle</returns>
        /// <response code="201">Returns the newly created UserGameTitle</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserGameTitleModel>> PostUserGameTitle(PostUserGameTitleModel postUserGameTitleModel)
        {
            try
            {
                GetUserGameTitleModel userGameTitle = await _userGameTitleRepository.PostUserGameTitle(postUserGameTitleModel);

                return CreatedAtAction(nameof(GetUserGameTitle), new { id = userGameTitle.Id }, userGameTitle);
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
        public async Task<IActionResult> DeleteUserGameTitle(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userGameTitleId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteUserGameTitle" + "400");
                }

                await _userGameTitleRepository.DeleteUserGameTitle(userGameTitleId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}
