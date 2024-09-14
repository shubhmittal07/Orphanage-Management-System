using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService){
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (status,token) = await _authService.Login(model);

            if(status==1){
                return Ok(new { token = token });

            }
            return Unauthorized();

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = model.UserRole;
            var (status,result) = await _authService.Register(model,role);

            if (status==0)
            {
                return StatusCode(500, new { Message = result });
            }
            if (status == 2)
            {
                return StatusCode(409, new { Message = result });
            }
            return Ok(new { Message = result });
        }
    }
}