using Core.Entities;
using Core.Repository;
using Core.Service;
using Core.Shared;
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

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public static string SecretKey = "401b09eab3c013d4ca54922bb802bec8fd5318192b0aAMK167d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        #region Hash Salt Password
        /// <summary>
        /// This Function Create Random Hashe Salt Password
        /// </summary>
        /// <param name="textPassword">Plan text Password</param>
        /// <param name="salt"></param>
        /// <returns>Hashe Salt Password</returns>
        public string CreateRandomHasheSaltPassword(out string textPassword, out byte[] salt)
        {
            Random randomNumber = new Random();
            textPassword = GeneralStringFunctions.GeneratePassword(10);
            return CreateHashSaltPassword(textPassword, out salt);
        }

        public string CreateHashSaltPassword(string password, out byte[] salt)
        {
            // generate a 128-bit salt using a secure PRNG
            salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashedSaltPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedSaltPassword;
        }

        public string GetHashSaltPassword(string password, byte[] salt)
        {

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
        #endregion

        #region Create Token
        private async Task<string> CreateUserToken(User user)
        {
            //set the time when it expires
            string expirationInMin = "2400";

            DateTime expires = DateTime.Now.AddMinutes(int.Parse(expirationInMin));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim("FristName", user.FristName));
            claimsIdentity.AddClaim(new Claim("LastName", user.LastName));
            claimsIdentity.AddClaim(new Claim("Email", user.Email));
            claimsIdentity.AddClaim(new Claim("Id", user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim("MobileNumber", user.MobileNo ?? ""));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsIdentity),
                SigningCredentials = credentials,
                Expires = expires
            };
            IdentityModelEventSource.ShowPII = true;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Validate Users
        /// <summary>
        /// Validate  User
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> ValidateUserAsync(string email, string password)
        {
            #region Validate User
            var user = await _userRepository.GetActiveUserByEmailAsync(email);
            if (user == null || GetHashSaltPassword(password, user.PasswordSalt) != user.Password)
                return null;
            #endregion

            #region Generate Token
            string token = await CreateUserToken(user);
            return token;
            #endregion
        }
        public async Task<User> ValidateUserMVCAsync(string email, string password)
        {
            #region Validate User
            var user = await _userRepository.GetActiveUserByEmailAsync(email);
            if (user == null || GetHashSaltPassword(password, user.PasswordSalt) != user.Password)
                return null;
            #endregion
            return user;
        }

        #endregion

    }
}
