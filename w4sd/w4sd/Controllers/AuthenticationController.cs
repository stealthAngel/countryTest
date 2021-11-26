using Microsoft.AspNetCore.Mvc;
using w4sd.Models;
using w4sd.Security;

namespace w4sd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly string _key;
        private readonly string _issuer;

        public AuthenticationController(ITokenService tokenService, IUserRepositoryService userRepositoryService, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
            _configuration = configuration;
            _key = _configuration["Jwt:Key"];
            _issuer = _configuration["Jwt:Issuer"];
            _logger = logger;
        }
        [HttpPost]
        public async Task Login(UserModel userModel)
        {
            var userDto = _userRepositoryService.GetUser(userModel);
            if (userDto == null)
            {
                HttpContext.Response.StatusCode = 401;
                _logger.LogError("Someone failed a login");
                return;
            }

            _logger.LogInformation("{userName} signed in", userModel.UserName);
            var token = _tokenService.BuildToken(_key, _issuer, userDto);
            await HttpContext.Response.WriteAsJsonAsync(new { token });
        }
    }
}

