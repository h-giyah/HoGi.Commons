using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HoGi.ToolsAndExtensions.Helpers
{
    public static class EncryptionHelper
    {
        const string _encryptionKey = "5616cd68-fbf8-4cf4-a897-05acffbbcd80";
        const string _salt = "c2fb8552-958f-441b-ad78-1bdec6ffb23d";
        public static string Encrypt(this string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);

            using var encryption = Aes.Create();

            var pdb = new Rfc2898DeriveBytes(_encryptionKey,
              Encoding.UTF8.GetBytes(_salt),
                256,
                HashAlgorithmName.SHA256);

            encryption.Key = pdb.GetBytes(32);

            encryption.IV = pdb.GetBytes(16);

            using var memoryStream = new MemoryStream();

            using (var cryptoStream = new CryptoStream(memoryStream, encryption.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                cryptoStream.Close();
            }

            var encryptedText = Convert.ToBase64String(memoryStream.ToArray());

            return encryptedText;
        }
        public static string ToClearText(this string cipherText)
        {

            cipherText = cipherText.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(cipherText);
            using var encryption = Aes.Create();

            var pdb = new Rfc2898DeriveBytes(_encryptionKey, Encoding.UTF8.GetBytes(_salt),
                256,
                HashAlgorithmName.SHA256);

            encryption.Key = pdb.GetBytes(32);
            encryption.IV = pdb.GetBytes(16);

            using var memoryStream = new MemoryStream();

            using (var cryptoStream = new CryptoStream(memoryStream, encryption.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.Close();
            }

            var clearText = Encoding.Unicode.GetString(memoryStream.ToArray());

            return clearText;
        }
    }
}
