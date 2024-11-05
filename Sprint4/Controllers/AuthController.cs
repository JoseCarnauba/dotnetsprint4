﻿namespace Sprint4.Controllers
{
    using global::Sprint4.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _authService.Authenticate(userLoginDto);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}