using Core.Entities;
using Core.Repository;
using Core.Service;
using Core.Shared;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SettingConfigService : ISettingConfigService
    {
        private readonly ISettingConfigRepository _settingConfigRepository;

        public SettingConfigService(ISettingConfigRepository settingConfigRepository)
        {
            _settingConfigRepository = settingConfigRepository;
        }

        #region Add || Edit
        public async Task<IEnumerable<SettingConfig>> AddEditSettingConfig(List<SettingConfig> settingConfigs)
        {
            var result = await _settingConfigRepository.AddEditSettingConfigs(settingConfigs);
            return result;
        }
        public async Task<SettingConfig> AddEditSettingConfig(SettingConfig settingConfigs)
        {
            var result = await _settingConfigRepository.AddEditSettingConfig(settingConfigs);
            return result;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<SettingConfig>> Get(string type)
        {
            var result = await _settingConfigRepository.GetAllSettingConfigs(type);
            return result;
        }
        #endregion

    }
}
