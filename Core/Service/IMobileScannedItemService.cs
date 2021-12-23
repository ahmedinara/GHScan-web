using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IMobileScannedItemService
    {

        #region Add
        Task<(string, Guid)> AddScannedItems(List<MobileScannedItemModel> mobileScannedItemModels, int userid);
        #endregion

        #region Get
        Task<IEnumerable<MobileScannedItem>> GetMobileScannedItemActive();
        Task<MobileScannedItem> GetMobileScannedItemById(int id);
        Task<IEnumerable<MobileScannedItem>> GetMobileScannedItemActiveWithGuId(Guid guid);
        #endregion

        #region Delete
        Task<bool> DeleteMobileScannedItemActive();
        #endregion
    }
}
