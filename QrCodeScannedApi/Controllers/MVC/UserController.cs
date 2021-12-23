using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Core.Models;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QrCodeScannedApi.Controllers.MVC
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserController(IAuthService authorizationService, IUserService userService)
        {
            _authService = authorizationService;
            _userService = userService;
        }
        // GET: UserController
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
    }
}
