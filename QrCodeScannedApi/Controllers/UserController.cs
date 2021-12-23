using Core.Entities;
using Core.Models;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeScannedApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserController(IAuthService authorizationService,IUserService userService)
        {
            _authService = authorizationService;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginModel loginModel)
        {
            var result = await _authService.ValidateUserAsync(loginModel.Email, loginModel.Password);
            if (result==null)
                return Unauthorized();
            return Ok(new { token = result });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            var enitiy = await _userService.AddUser(user);
            return Ok(enitiy);

        }
    }
}
