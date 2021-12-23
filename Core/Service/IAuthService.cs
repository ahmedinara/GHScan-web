using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IAuthService
    {
        #region Hash Salt Password
        string CreateRandomHasheSaltPassword(out string textPassword, out byte[] salt);

        string CreateHashSaltPassword(string password, out byte[] salt);

        string GetHashSaltPassword(string password, byte[] salt);
        #endregion

        #region Validate Users
        Task<string> ValidateUserAsync(string email, string password);
        Task<User> ValidateUserMVCAsync(string email, string password);
        #endregion
    }
}
