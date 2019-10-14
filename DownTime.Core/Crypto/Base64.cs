using System;
using System.Text;

namespace DownTime.Core.Crypto
{
    public class Base64 : IKeyProvider
    {
        public string Decrypt(string value)
        {
            var base64EncodedBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string Decrypt(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public string Encrypt(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string Encrypt(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
