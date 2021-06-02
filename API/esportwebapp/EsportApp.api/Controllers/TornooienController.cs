using EsportApp.api.Repositories;
using EsportApp.models.Tornooien;
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
    public class TornooienController : ControllerBase
    {
        private readonly ITornooiRepository _tornooiRepository;

        public TornooienController(ITornooiRepository tornooiRepository)
        {
            _tornooiRepository = tornooiRepository;
        }

        /// <summary>
        /// Get a list of all Tornooien.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tornooien
        ///
        /// </remarks>
        /// <returns>GetTornooienModel</returns>
        /// <response code="200">Returns the list of Tornooien</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooienModel>> GetTornooien()
        {
            return await _tornooiRepository.GetTornooien();
        }

        /// <summary>
        /// Get details of an Tornooi.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tornooien/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetTornooiModel</returns>
        /// <response code="200">Returns the Tornooi</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooiModel>> GetTornooi(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid tornooiId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "GetTornooi" + "400");
                }

                GetTornooiModel tornooi = await _tornooiRepository.GetTornooi(tornooiId);

                return tornooi;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an Tornooi.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/tornooien
        ///     {
        ///        "Naam": "Name of the tornooi",
        ///        "Organisator": "Name of the organisator",
        ///        "beschrijving": "beschrijving van het tornooi",
        ///        "GameTitleId": "Id of the gameTitle",
        ///        "Type": "Type of the tornooi",
        ///     }
        ///
        /// </remarks>
        /// <param name="postTornooiModel"></param>
        /// <returns>A newly created Tornooi</returns>
        /// <response code="201">Returns the newly created Tornooi</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetTornooiModel>> PostTornooi(PostTornooiModel postTornooiModel)
        {
            try
            {
                GetTornooiModel tornooi = await _tornooiRepository.PostTornooi(postTornooiModel);

                return CreatedAtAction(nameof(GetTornooi), new { id = tornooi.Id }, tornooi);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Updates an Tornooi.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/tornooien/{id}
        ///     {
        ///        "Naam": "Name of the tornooi",
        ///        "Organisator": "Name of the organisator",
        ///        "beschrijving": "beschrijving van het tornooi",
        ///        "GameTitleId": "Id of the gameTitle",
        ///        "Type": "Type of the tornooi",
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <param name="putGameModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> PutTornooi(string id, PutTornooiModel putTornooiModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid tornooiId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "PutTornooi" + "400");
                }

                await _tornooiRepository.PutTornooi(tornooiId, putTornooiModel);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }


        /// <summary>
        /// Deletes an Tornooi.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/tornooien/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The Tornooi could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTornooi(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid tornooiId))
                {
                    throw new Exception("Ongeldig id" + this.GetType().Name + "DeleteTornooi" + "400");
                }

                await _tornooiRepository.DeleteTornooi(tornooiId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}

