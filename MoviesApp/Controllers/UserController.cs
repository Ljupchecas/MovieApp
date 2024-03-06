using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTOs.UserDto;
using MoviesApp.Services.Interfaces;
using MoviesApp.Shared.CustomExceptions;

namespace MoviesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDto loginDto)
        {
            try
            {
                string token = _userService.Login(loginDto);
                return Ok(new ResponseDto() { Success = token});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Error = "An error occured!"});
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                _userService.RegisterUser(registerDto);
                return Ok(new ResponseDto() { Success = "Succesfully registerd user!" });
            }
            catch(UserException ex)
            {
                return BadRequest(new ResponseDto() { Error = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginUser = _userService.Authenticate(loginDto);

                if (loginUser == null)
                {
                    return NotFound("Username or Password is incorrect!");
                }
                return Ok(loginUser);
            }
            catch (UserException ex)
            {
                return BadRequest(new ResponseDto() { Error = ex.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Error = "An error occured!" });

            }
        }
    }
}
