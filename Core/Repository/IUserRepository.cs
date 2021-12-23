using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IUserRepository
    {
        #region Add
        Task<User> AddUser(User user);
        #endregion

        #region Get  
        Task<User> GetActiveUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        #endregion

        #region Update
        #endregion
    }
}
