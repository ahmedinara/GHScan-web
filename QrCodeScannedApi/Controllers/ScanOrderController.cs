using Core.Entities;
using Core.Models;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeScannedApi.Controllers
{
    public class ScanOrderController : Controller
    {
        private readonly IMobileScannedItemService _mobileScannedItemService;
        private readonly IScannedHeaderService _scannedHeaderService;
        private readonly ISettingConfigService _settingConfigService;

        public ScanOrderController(IMobileScannedItemService mobileScannedItemService,IScannedHeaderService scannedHeaderService,ISettingConfigService settingConfigService)
        {
            _mobileScannedItemService = mobileScannedItemService;
            _scannedHeaderService = scannedHeaderService;
            _settingConfigService = settingConfigService;
        }
        // GET: ScannedItemController
        public ActionResult successfull()
        {
            return View();
        }
        public ActionResult Fail()
        {
            return View();
        }

        // GET: ScannedItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ScannedItemController/Create
        //[Route("Scann")]
        public async Task<ActionResult> View([FromQuery] Guid guid)
        {

            
            var mobileScanned = await _mobileScannedItemService.GetMobileScannedItemActiveWithGuId(guid);
            return View(mobileScanned);
        }

        // POST: ScannedItemController/Create
        [HttpPost]
        //[Route("Scann/Open")]
        public async Task<string> View([FromQuery] Guid guid, [FromQuery] string mail, [FromBody] List<MobileScannedViewModel> mobileScannedItems)
        {
            try
            {
                if (mail == null)
                    return "0";
                SettingConfig settingConfig = new SettingConfig { SettingKey = "To", SettingGroup = "Mail", SettingValue = mail };
                var setting = await _settingConfigService.AddEditSettingConfig(settingConfig);
                var result = await _scannedHeaderService.AddScannedHeader(mobileScannedItems,1,"ahmedinara00@gmail.com", "eng.hanan.abdelwahab@gmail.com", guid);
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        public async Task<IActionResult> Finsh([FromQuery] string guid)
        {
            ViewData["guid"] = guid;
            return View();
        }
        public async Task<IActionResult> Csv([FromQuery] Guid guid)
        {
            try
            {
                var scanned = await _scannedHeaderService.GetScannedHeaderByGUIdAsync(guid);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Qr Code,QrCode Format (GTIN;SN;BN;XD)");
                foreach (var scannedDetial in scanned.ScannedDetials)
                {
                    stringBuilder.AppendLine($"{scannedDetial.QrCode},{scannedDetial.QrFormat}");
                }

                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "Product.csv");
                
            }
            catch
            {
                return View();
            }
        }
        // GET: ScannedItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ScannedItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ScannedItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ScannedItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
