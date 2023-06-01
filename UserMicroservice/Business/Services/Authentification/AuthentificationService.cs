
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Business.Services.Authentification
{
    public class AuthentificationService:IAuthentificationService
    {

        public AuthentificationService() {
            

        }
        public string EncryptString(string plainText, byte[] key, byte[] iv)
        {
           
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    byte[] cipherTextBytes = ms.ToArray();
                    return Convert.ToBase64String(cipherTextBytes);
                }
            }
        }
    }
}
