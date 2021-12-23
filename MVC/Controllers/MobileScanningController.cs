using Core.Models;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class MobileScanningController : Controller
    {
        private readonly IMobileScannedItemService _mobileScannedItemService;

        public MobileScanningController(IMobileScannedItemService mobileScannedItemService)
        {
            _mobileScannedItemService = mobileScannedItemService;
        }
        // GET: MobileScanningController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MobileScanningController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MobileScanningController/Create
        public async Task<ActionResult> Create()
        {
            var mobile = await _mobileScannedItemService.GetMobileScannedItemActive();
            return View(mobile);
        }

        // POST: MobileScanningController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MobileScannedItemModel mobileScannedItemModel)
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

        // GET: MobileScanningController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MobileScanningController/Edit/5
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

        // GET: MobileScanningController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MobileScanningController/Delete/5
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
