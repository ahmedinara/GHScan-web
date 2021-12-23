using Core.Entities;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        #region Add
        public async Task<User> AddUser(User user)
        {
            user.Password = _authService.CreateHashSaltPassword(user.Password, out byte[] salt);
            user.PasswordSalt = salt;
            user.CreatedOn = DateTime.Now;
            var entity = await _userRepository.AddUser(user);
            return entity;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        #endregion



    }
}
