using Core.Models;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QrCodeScannedApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
    {
        private readonly IMobileScannedItemService _mobileScannedItemService;

        public ScanController(IMobileScannedItemService mobileScannedItemService)
        {
            _mobileScannedItemService = mobileScannedItemService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<object>> Post([FromBody] List<MobileScannedItemModel> mobileScannedItemModels)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            string currentUserid = identity.FindFirst("Id").Value;
            (string url,Guid guid) result = await _mobileScannedItemService.AddScannedItems(mobileScannedItemModels,int.Parse(currentUserid));

            return Ok(new {url = result.url.Trim(), guid= result.guid.ToString() } );
        }

        // PUT api/<MobileScannedController>/5
     
    }
}
