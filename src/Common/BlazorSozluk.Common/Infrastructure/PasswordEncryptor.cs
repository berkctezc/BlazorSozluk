using System.Security.Cryptography;
using System.Text;

namespace BlazorSozluk.Common.Infrastructure;

public class PasswordEncryptor
{
    public static string Encrypt(string password)
    {
        using var md5 = MD5.Create();

        var inputBytes = Encoding.ASCII.GetBytes(password);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}