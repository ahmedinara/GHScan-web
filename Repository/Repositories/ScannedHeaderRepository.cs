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
    public class ScannedHeaderRepository : IScannedHeaderRepository
    {
        private readonly AppDbContext _dbContext;

        public ScannedHeaderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Add
        public async Task<ScannedHeader> AddScannedHeader(ScannedHeader scannedHeader)
        {
            try
            {
             var entity=  await _dbContext.ScannedHeaders.AddAsync(scannedHeader);
               await Commit();
               return entity.Entity;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Get
        public async Task<IEnumerable<ScannedHeader>> GetScannedHeaderAsync()
        {
            return await _dbContext.ScannedHeaders.ToListAsync();
        }
        public async Task<ScannedHeader> GetScannedHeaderByGUIdAsync(Guid guid)
        {
            return await _dbContext.ScannedHeaders.Include(I=>I.ScannedDetials).AsNoTracking().FirstOrDefaultAsync(s=>s.ScannedGuid==guid);
        }

        public async Task<ScannedHeader> GetScannedHeaderId(int id)
        {
            return await _dbContext.ScannedHeaders.Include(s=>s.ScannedDetials).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
        #endregion

        #region Delete
        //public async Task<bool> DeleteMobileScannedItemIsFinshed()
        //{
        //    try
        //    {
        //        var mobileScannedItems = await _dbContext.MobileScannedItems.Where(m => m.IsFinshed == true).ToListAsync();
        //        _dbContext.MobileScannedItems.RemoveRange(mobileScannedItems);
        //        await Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var x = ex.ToString();
        //        return false;
        //    }
        //}
        #endregion

        #region Commit
        private async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}

