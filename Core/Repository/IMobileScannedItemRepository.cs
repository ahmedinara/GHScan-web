using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IMobileScannedItemRepository
    {

        #region Add
        Task<IEnumerable<MobileScannedItem>> AddMobileScannedItem(List<MobileScannedItem> mobileScannedItems);
        #endregion

        #region Get
        Task<List<MobileScannedItem>> GetMobileScannedItemsAsync(bool? isFinshed);
        Task<List<MobileScannedItem>> GetMobileScannedItemsbyGUIdAsync(bool? isFinshed, Guid guid);
        Task<MobileScannedItem> GetMobileScannedItemId(int id);
        Guid GetMobileScannedItem();
        #endregion

        #region Delete
        Task<bool> DeleteMobileScannedItemIsFinshed();
        #endregion
    }
}
