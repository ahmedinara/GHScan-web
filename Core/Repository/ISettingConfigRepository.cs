using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface ISettingConfigRepository
    {
        #region AddEdit
        Task<IEnumerable<SettingConfig>> AddEditSettingConfigs(List<SettingConfig> settingConfigs);
        Task<SettingConfig> AddEditSettingConfig(SettingConfig settingConfig);
        #endregion
        #region Get
        Task<IEnumerable<SettingConfig>> GetAllSettingConfigs(string type);
        #endregion
    }
}
