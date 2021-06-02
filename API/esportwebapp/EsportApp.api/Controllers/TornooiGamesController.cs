using EsportApp.api.Repositories;
using EsportApp.models.TornooiGames;
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
    public class TornooiGamesController : ControllerBase
    {
        private readonly ITornooiGameRepository _tornooiGameRepository;

        public TornooiGamesController(ITornooiGameRepository tornooiGameRepository)
        {
            _tornooiGameRepository = tornooiGameRepository;
        }

        /// <summary>
        /// Get a list of all TornooiGames.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tornooiGames
        ///
        /// </remarks>
        /// <returns>GetTornooiGamesModel</returns>
        /// <response code="200">Returns the list of TornooiGames</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooiGamesModel>> GetTornooiGames()
        {
            return await _tornooiGameRepository.GetTornooiGames();
        }

        /// <summary>
        /// Get details of an TornooiGame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tornooiGames/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetTornooiGameModel</returns>
        /// <response code="200">Returns the TornooiGame</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooiGameModel>> GetTornooiGame(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid tornooiGameId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetTornooiGame" + "400");
                }

                GetTornooiGameModel tornooiGame = await _tornooiGameRepository.GetTornooiGame(tornooiGameId);

                return tornooiGame;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        //    /// <summary>
        //    /// Creates an TornooiGame.
        //    /// </summary>
        //    /// <remarks>
        //    /// Sample request:
        //    ///
        //    ///     POST /api/tornooiGames
        //    ///     {
        //    ///        "Naam": "Name of the tornooi",
        //    ///        "Organisator": "Name of the organisator",
        //    ///        "beschrijving": "beschrijving van het tornooi",
        //    ///        "GameTitleId": "Id of the gameTitle",
        //    ///        "Type": "Type of the tornooi",
        //    ///     }
        //    ///
        //    /// </remarks>
        //    /// <param name="postTornooiModel"></param>
        //    /// <returns>A newly created Tornooi</returns>
        //    /// <response code="201">Returns the newly created Tornooi</response>
        //    /// <response code="400">If something went wrong while saving into the database</response>    
        //    /// <response code="401">Unauthorized - Invalid JWT token</response> 
        //    /// <response code="403">Forbidden - Required role assignment is missing</response> 
        //    [HttpPost]
        //    [ProducesResponseType(StatusCodes.Status201Created)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    //[Authorize(Roles = "Beheerder")]
        //    [AllowAnonymous]
        //    public async Task<ActionResult<GetTornooiModel>> PostTornooi(PostTornooiModel postTornooiModel)
        //    {
        //        try
        //        {
        //            GetTornooiModel tornooi = await _tornooiRepository.PostTornooi(postTornooiModel);

        //            return CreatedAtAction(nameof(GetTornooi), new { id = tornooi.Id }, tornooi);
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest("" + e);
        //        }
        //    }

        //    /// <summary>
        //    /// Updates an Tornooi.
        //    /// </summary>
        //    /// <remarks>
        //    /// Sample request:
        //    ///
        //    ///     PUT /api/tornooien/{id}
        //    ///     {
        //    ///        "Naam": "Name of the tornooi",
        //    ///        "Organisator": "Name of the organisator",
        //    ///        "beschrijving": "beschrijving van het tornooi",
        //    ///        "GameTitleId": "Id of the gameTitle",
        //    ///        "Type": "Type of the tornooi",
        //    ///     }
        //    ///
        //    /// </remarks>
        //    /// <param name="id"></param>      
        //    /// <param name="putGameModel"></param>   
        //    /// <response code="204">Returns no content</response>
        //    /// <response code="404">The game could not be found</response> 
        //    /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        //    /// <response code="401">Unauthorized - Invalid JWT token</response> 
        //    /// <response code="403">Forbidden - Required role assignment is missing</response> 
        //    [HttpPut("{id}")]
        //    [ProducesResponseType(StatusCodes.Status204NoContent)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    //[Authorize(Roles = "Beheerder")]
        //    [AllowAnonymous]
        //    public async Task<IActionResult> PutTornooi(string id, PutTornooiModel putTornooiModel)
        //    {
        //        try
        //        {
        //            if (!Guid.TryParse(id, out Guid tornooiId))
        //            {
        //                throw new Exception("Ongeldig id" + this.GetType().Name + "PutTornooi" + "400");
        //            }

        //            await _tornooiRepository.PutTornooi(tornooiId, putTornooiModel);

        //            return NoContent();
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest("" + e);
        //        }
        //    }


        //    /// <summary>
        //    /// Deletes an Tornooi.
        //    /// </summary>
        //    /// <remarks>
        //    /// Sample request:
        //    ///
        //    ///     DELETE /api/tornooien/{id}
        //    ///
        //    /// </remarks>
        //    /// <param name="id"></param>      
        //    /// <response code="204">Returns no content</response>
        //    /// <response code="404">The Tornooi could not be found</response> 
        //    /// <response code="400">The id is not a valid Guid</response> 
        //    /// <response code="401">Unauthorized - Invalid JWT token</response> 
        //    /// <response code="403">Forbidden - Required role assignment is missing</response> 
        //    [HttpDelete("{id}")]
        //    [ProducesResponseType(StatusCodes.Status204NoContent)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    //[Authorize(Roles = "Beheerder")]
        //    [AllowAnonymous]
        //    public async Task<IActionResult> DeleteTornooi(string id)
        //    {
        //        try
        //        {
        //            if (!Guid.TryParse(id, out Guid tornooiId))
        //            {
        //                throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteTornooi" + "400");
        //            }

        //            await _tornooiRepository.DeleteTornooi(tornooiId);

        //            return NoContent();
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest("" + e);
        //        }
        //    }
        //}
    }
}
