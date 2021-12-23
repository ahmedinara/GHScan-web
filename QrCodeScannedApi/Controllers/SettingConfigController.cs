using Core.Entities;
using Core.Models;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeScannedApi.Controllers
{
    public class SettingConfigController : Controller
    {
        private readonly ISettingConfigService _settingConfigService;

        public SettingConfigController(ISettingConfigService settingConfigService)
        {
            _settingConfigService = settingConfigService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _settingConfigService.Get("Mail");
            ServerSetting serverSetting = new ServerSetting
            {
                Host = result.FirstOrDefault(s => s.SettingKey == "Host").SettingValue,
                mailForm = result.FirstOrDefault(s => s.SettingKey == "From").SettingValue,
                mailTo = result.FirstOrDefault(s => s.SettingKey == "To").SettingValue,
                Password = result.FirstOrDefault(s => s.SettingKey == "Password").SettingValue,
                Port = result.FirstOrDefault(s => s.SettingKey == "Port").SettingValue,
            };
            return View(serverSetting);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ServerSetting serverSetting)
        {
            List<SettingConfig> settingConfigs = new List<SettingConfig>();
            settingConfigs.Add(new SettingConfig { SettingKey = "Host", SettingGroup = "Mail", SettingValue = serverSetting.Host });
            settingConfigs.Add(new SettingConfig { SettingKey = "From", SettingGroup = "Mail", SettingValue = serverSetting.mailForm });
            settingConfigs.Add(new SettingConfig { SettingKey = "To", SettingGroup = "Mail", SettingValue = serverSetting.mailTo });
            settingConfigs.Add(new SettingConfig { SettingKey = "Port", SettingGroup = "Mail", SettingValue = serverSetting.Port });
            settingConfigs.Add(new SettingConfig { SettingKey = "Password", SettingGroup = "Mail", SettingValue = serverSetting.Password });
            var settingconfigs = await _settingConfigService.AddEditSettingConfig(settingConfigs);
            if(settingConfigs.Count>0)
            return View(serverSetting);
            else
                return View();

        }
    }
}
