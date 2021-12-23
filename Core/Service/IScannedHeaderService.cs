using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IScannedHeaderService
    {

        #region Add
        Task<ScannedHeader> AddScannedHeader(List<MobileScannedViewModel> mobileScannedItem, int userid, string sentMail, string recivedMain, Guid guid);
        #endregion

        #region Get
        Task<IEnumerable<ScannedHeader>> GetMobileScannedItemActive();
        Task<ScannedHeader> GetMobileScannedItemById(int id);
        Task<ScannedHeader> GetScannedHeaderByGUIdAsync(Guid guid);
        #endregion
    }
}
