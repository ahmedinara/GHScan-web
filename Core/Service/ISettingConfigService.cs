using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ISettingConfigService
    {
        #region add|| edit
        Task<IEnumerable<SettingConfig>> AddEditSettingConfig(List<SettingConfig> settingConfigs);
        Task<SettingConfig> AddEditSettingConfig(SettingConfig settingConfigs);
        #endregion
        #region get
        Task<IEnumerable<SettingConfig>> Get(string type);
        #endregion
    }
}
