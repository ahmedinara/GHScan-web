using Microsoft.AspNetCore.Mvc;
using QrCodeScannedApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeScannedApi.Controllers.MVC
{
    
    public class HomeController : Controller
    {
        [HttpPost]
        public string CheckToken(string token)
        {
            var tokenCheckResult = ValidateToken.ValidateCurrentToken(token);
            if (!tokenCheckResult) return "0";
            else return "1";
        }
        public IActionResult Index()
        {

           //// var result = await LocalStorage.GetAsync<string>(_localStorage);
           // var tokenCheckResult = ValidateToken.ValidateCurrentToken(access_token);
           // if (!tokenCheckResult) return Redirect("/User/Login");
            return View();
        }
    }
}
