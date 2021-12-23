using Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using Core.Entities;

namespace Repository.Repositories
{
    public class SettingConfigRepository : ISettingConfigRepository
    {
        private readonly AppDbContext _dbContext;

        public SettingConfigRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Add
        public async Task<IEnumerable<SettingConfig>> AddEditSettingConfigs(List<SettingConfig> settingConfigs)
        {
            try
            {
                foreach (var settingConfig in settingConfigs)
                {
                    var setting = await _dbContext.SettingConfigs.AsNoTracking().FirstOrDefaultAsync(s => s.SettingKey == settingConfig.SettingKey);
                    if (setting == null)
                        await _dbContext.SettingConfigs.AddAsync(settingConfig);
                    else
                         _dbContext.SettingConfigs.Update(settingConfig);
                }
                await Commit();
                return settingConfigs;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }
        }
        public async Task<SettingConfig> AddEditSettingConfig(SettingConfig settingConfig)
        {
            try
            {
                var setting = await _dbContext.SettingConfigs.AsNoTracking().FirstOrDefaultAsync(s => s.SettingKey == settingConfig.SettingKey);
                if (setting == null)
                    await _dbContext.SettingConfigs.AddAsync(settingConfig);
                else
                    _dbContext.SettingConfigs.Update(settingConfig);

                await Commit();
                return settingConfig;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Get
        public async Task<IEnumerable<SettingConfig>> GetAllSettingConfigs(string type)
        {
            return await _dbContext.SettingConfigs.Where(s=>s.SettingGroup==type).ToListAsync();
        }
      
        #endregion

        #region Update
        #endregion

        #region Commit
        private async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}

