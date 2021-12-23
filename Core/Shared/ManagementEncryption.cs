using System;
using System.Text;
namespace Core.Shared
{
    
    public static class ManagementEncryption
    {
        private readonly static string encryptionKey = "E546C8DF278CD5931069B522E695D4F2";

        #region Encrypt and Decrypt
        public static string EncryptPassword(string password)
        {
            password = password + encryptionKey;
            var encryptedPassword = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encryptedPassword);
        }
        public static string DecryptPassword(string encryptedPassword)
        {
            var baasEncodeBytes = Convert.FromBase64String(encryptedPassword);
            var result = Encoding.UTF8.GetString(baasEncodeBytes);
            result = result.Substring(0, result.Length - encryptionKey.Length);
            return result;
        }
        #endregion

    }
}
