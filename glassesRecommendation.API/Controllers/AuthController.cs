using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace glassesRecommendation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var authResponse = await _authenticationService.RegisterAsync(registerDto, cancellationToken);
            if (authResponse.IsSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
					Path = "/",
					Secure = true,
					SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(1)
                };
                Response.Cookies.Append("token", authResponse.Token);
                return Ok(authResponse);
            }

            return BadRequest(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var authResponse = await _authenticationService.LoginAsync(loginDto, cancellationToken);
            if (authResponse.IsSuccess)
            {
				var cookieOptions = new CookieOptions
				{
					HttpOnly = false,
                    Path = "/",
					Secure = true,
					SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(1)
				};
				Response.Cookies.Append("token", authResponse.Token);
				return Ok(authResponse);
            }
            return Unauthorized(authResponse);
        }

        [HttpGet("check")]
        public IActionResult CheckAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { isAuthenticated = true });
            }
            return Unauthorized(new { isAuthenticated = false });
        }
    }
}
