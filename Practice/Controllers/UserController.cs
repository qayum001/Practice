using Microsoft.AspNetCore.Mvc;
using Practice.Data.Dto;
using Practice.Services.AuthService;
using Practice.Services.CheckerService;
using Practice.Services.CheckerService.ILoginCheckerService;
using Practice.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Practice.Services.TokenService;
using Practice.Services.UserService;
using FluentValidation;

namespace Practice.Controllers
{
    [Route("api/account")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRegisterCheckerService _registerCheckerService;
        private readonly ILoginCheckService _loginCheckService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UserController(IAuthService authService,
            IRegisterCheckerService registerCheckerService,
            ILoginCheckService loginCheckService,
            ITokenService tokenService,
            IUserService userService,
            IValidator<UserRegisterDto> registerValidator
            )
        {
            _authService = authService;
            _registerCheckerService = registerCheckerService;
            _loginCheckService = loginCheckService;
            _tokenService = tokenService;
            _userService = userService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userRegisterDto"></param>
        /// <returns>A string token</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPost("register")]
        public async Task<ActionResult<TokenDto>> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (_registerCheckerService.IsEmailExists(userRegisterDto.Email).Result) return StatusCode(400, "Email already exists");

            var token = new TokenDto()
            {
                Token = await _authService.Register(userRegisterDto)
            };
        
            return Ok(token);
        }

        /// <summary>
        /// Log in to the system
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns>A string token</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Email or password is incorrect</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login(LoginCredentials loginCredentials) 
        {
            if (!ModelState.IsValid) { return StatusCode(400); };

            if (!await _registerCheckerService.IsEmailExists(loginCredentials.Email)
                || !await _loginCheckService.IsLoginCorrect(loginCredentials)) { return StatusCode(401); }

            var token = await _authService.LogIn(loginCredentials);

            return Ok(token);
        }

        /// <summary>
        /// Log out system user
        /// </summary>
        /// <returns>Response</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<Response>> Logout() 
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];

            var isValid = await _tokenService.IsTokenValid(token);

            if (!isValid) return StatusCode(401);

            await _authService.LogOut(token);

            return Ok(new Response()
            {
                Stasus = "Log out",
                Message = "your token are no valid now"
            });
            
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <returns>Response</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetProfile() 
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var isValid = await _tokenService.IsTokenValid(token);

            if (!isValid) return StatusCode(401);

            var id = await _tokenService.GetGuid(token);

            var userDto = await _userService.GetUserProfileAsync(id);

            return Ok(userDto);
        }

        /// <summary>
        /// Edit profile
        /// </summary>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<Response>> EditProfile([FromBody] EditUserDto editUserDto)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var isValid = await _tokenService.IsTokenValid(token);

            if (!isValid) return StatusCode(401);

            var id = await _tokenService.GetGuid(token);

            //todo: add validator here or in userService

            var response = await _userService.EditUserProfileAsync(id, editUserDto);

            return Ok(response);
        }
    }
}