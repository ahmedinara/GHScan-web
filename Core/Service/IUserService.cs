using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IUserService
    {
        #region Add
        Task<User> AddUser(User user);
        #endregion

        #region Get  
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        #endregion

        #region Update
        #endregion
    }
}
