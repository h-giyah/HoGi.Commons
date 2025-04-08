using System;
using System.Security.Cryptography;
using System.Text;

namespace HoGi.Commons.ToolsAndExtensions.Helpers;

public class LegacyPasswordHasher
{
    private const int SaltSize = 16, HashSize = 20, HashIter = 128;

    private readonly byte[] _salt, _hash;
    public string Hash => BitConverter.ToString(_hash);

    public LegacyPasswordHasher(string password)
    {
        var passwordBytes = Encoding.ASCII.GetBytes(password);

        _salt = new byte[SaltSize];

        Array.Copy(passwordBytes, _salt, passwordBytes.Length > 16 ? 16 : passwordBytes.Length);

        using var sha = SHA256.Create();

        _hash = sha.ComputeHash(new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize));
    }

    public bool Verify(string password)
    {
        using var sha = SHA256.Create();
        {
            var test = sha.ComputeHash(new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize));

            for (var i = 0; i < HashSize; i++)
                if (test[i] != _hash[i])
                    return false;
            return true;
        }
    }
}
