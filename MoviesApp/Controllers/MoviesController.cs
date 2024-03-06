using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTOs;
using MoviesApp.Services.Interfaces;
using MoviesApp.Shared.CustomExceptions;
using System.Security.Claims;

namespace MoviesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new UserException(userId, name,
                    "Name identifier claim does not exist!");
            }
            return userId;
        }

        [HttpGet]
        public ActionResult<List<MovieDto>> GetAllMovies()
        {
            try
            {
                var userId = GetAuthorizedUserId();
                return Ok(_movieService.GetAllMovies(userId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getMovieByIdQueryString")]
        public ActionResult<MovieDto> GetByIdQuery(int id)
        {
            try
            {
                return Ok(_movieService.GetMovieById(id));
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetMovieByRouteParm(int id)
        {
            try
            {
                return Ok(_movieService.GetMovieById(id));
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("createMovie")]
        public IActionResult CreateMovie([FromBody] CreateMovieDto createMovieDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _movieService.CreateMovie(createMovieDto, userId);
                return StatusCode(StatusCodes.Status201Created, "Movie added");
            }
            catch (MovieNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("deleteMovie")]
        public ActionResult DeleteMovie(int id)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _movieService.DeleteMovie(id, userId);
                return StatusCode(StatusCodes.Status204NoContent, "Movie deleted");
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("updateMovie")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto updateMovieDto)
        {
            try
            {
                _movieService.UpdateMovie(updateMovieDto);
                return Ok("Movie successfully updated.");
            }
            catch (MovieNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<MovieDto>> FilterMovies(int? year, int? genre)
        {
            try
            {
                return Ok(_movieService.Filter(year, genre));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
