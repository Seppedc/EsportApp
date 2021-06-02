using EsportApp.api.Repositories;
using EsportApp.models.Games;
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
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        /// <summary>
        /// Get a list of all Games.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/games
        ///
        /// </remarks>
        /// <returns>GetGamesModel</returns>
        /// <response code="200">Returns the list of Games</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGamesModel>> GetGames()
        {
            return await _gameRepository.GetGames();
        }

        /// <summary>
        /// Get details of an Game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/games/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <returns>An GetGameModel</returns>
        /// <response code="200">Returns the game</response>
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Leerling")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameModel>> GetGame(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "GetGame"+ "400");
                }

                GetGameModel game = await _gameRepository.GetGame(gameId);

                return game;
            }
            catch (Exception e)
            {
                throw new Exception("" + e);
            }
        }

        /// <summary>
        /// Creates an game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/games
        ///     {
        ///        "Naam": "Name of the game"
        ///     }
        ///
        /// </remarks>
        /// <param name="postGameModel"></param>
        /// <returns>A newly created game</returns>
        /// <response code="201">Returns the newly created game</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<ActionResult<GetGameModel>> PostGame(PostGameModel postGameModel)
        {
            try
            {
                GetGameModel game = await _gameRepository.PostGame(postGameModel);

                return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        /// <summary>
        /// Updates an game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/games/{id}
        ///     {
        ///        "Naam": "Name of the game"
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
        public async Task<IActionResult> PutGame(string id, PutGameModel putGameModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "PutGame"+ "400");
                }

                await _gameRepository.PutGame(gameId, putGameModel);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    

        /// <summary>
        /// Deletes an game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/games/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>      
        /// <response code="204">Returns no content</response>
        /// <response code="404">The game could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Beheerder")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteGame(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid gameId))
                {
                    throw new Exception("Ongeldig id"+ this.GetType().Name+ "DeleteGame"+ "400");
                }

                await _gameRepository.DeleteGame(gameId);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }
}


