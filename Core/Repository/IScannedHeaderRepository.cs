using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IScannedHeaderRepository
    {

        #region Add
        Task<ScannedHeader> AddScannedHeader(ScannedHeader scannedHeader);
        #endregion

        #region Get
        Task<IEnumerable<ScannedHeader>> GetScannedHeaderAsync();
        Task<ScannedHeader> GetScannedHeaderByGUIdAsync(Guid guid);
        Task<ScannedHeader> GetScannedHeaderId(int id);
        #endregion
    }
}
