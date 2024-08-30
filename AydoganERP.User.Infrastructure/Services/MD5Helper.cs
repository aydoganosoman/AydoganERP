using AydoganERP.Base.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AydoganERP.User.Infrastructure.Services;

public class MD5Helper : IMD5Helper
{
    public string GenerateMD5(string input)
    {
        MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();

    }
}
