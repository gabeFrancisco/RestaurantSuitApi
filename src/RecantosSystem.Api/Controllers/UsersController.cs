using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Services.Logging;

namespace RecantosSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly LogService _logService;
        public UsersController(IUserService userService, LogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        /// <summary>
        /// Registers a new user to the system.
        /// </summary>
        /// <param name="userDto">The User data transfer object to register the new one.</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The user data is incorrect!");
            }
            return Ok(await _userService.RegisterUser(userDto));
        }

        /// <summary>
        /// Logs an existing user in the system.
        /// </summary>
        /// <param name="loginDto">The login data transfer object.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The login data is incorrect");
            }

            var result = await _userService.Login(loginDto);
            Response.Cookies.Append("token", result.Token, result.Cookies);

            return Ok(result);
        }

        /// <summary>
        /// Get a user with a given id. This route is protected!
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserDTO</returns>
        // [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _userService.GetAsync(id));
        }

        /// <summary>
        /// Get the actual user.
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _userService.GetActualUser());
        }
    }
}