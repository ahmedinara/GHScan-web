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
    public class MobileScannedItemRepository : IMobileScannedItemRepository
    {
        private readonly AppDbContext _dbContext;

        public MobileScannedItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Add
        public async Task<IEnumerable<MobileScannedItem>> AddMobileScannedItem(List<MobileScannedItem> mobileScannedItems)
        {
            try
            {
               await _dbContext.MobileScannedItems.AddRangeAsync(mobileScannedItems);
               await Commit();
               return mobileScannedItems;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Get
        public async Task<List<MobileScannedItem>> GetMobileScannedItemsbyGUIdAsync(bool? isFinshed,Guid guid)
        {
            var mobileScannedItems = _dbContext.MobileScannedItems.Where(m=>m.ScannedGuid==guid).AsQueryable();
            if (isFinshed != null)
                mobileScannedItems.Where(m => m.IsFinshed == isFinshed);
            return await mobileScannedItems.ToListAsync();
        }
        public async Task<List<MobileScannedItem>> GetMobileScannedItemsAsync(bool? isFinshed)
        {
            var mobileScannedItems =  _dbContext.MobileScannedItems.AsQueryable();
            if (isFinshed != null)
                mobileScannedItems.Where(m => m.IsFinshed == isFinshed);
            return await mobileScannedItems.ToListAsync();
        }

        public async Task<MobileScannedItem> GetMobileScannedItemId(int id)
        {
            return await _dbContext.MobileScannedItems.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
        public Guid GetMobileScannedItem()
        {
            return _dbContext.MobileScannedItems.AsNoTracking().FirstOrDefault().ScannedGuid;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteMobileScannedItemIsFinshed()
        {
            try
            {
                var mobileScannedItems = await _dbContext.MobileScannedItems.Where(m => m.IsFinshed == false).ToListAsync();
                _dbContext.MobileScannedItems.RemoveRange(mobileScannedItems);
                await Commit();
                return true;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return false;
            }
        }
        #endregion

        #region Commit
        private async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}

