using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DownTime.Core.Hash
{
    public class Md5Hash : IHash
    {
        public override string HashText(string value)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var computedHashes = md5.ComputeHash(Encoding.Default.GetBytes(value));

                return GetHashes(computedHashes);
            }
        }

        public override string HashFile(string filePath)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var computedHashes = md5.ComputeHash(File.ReadAllBytes(filePath));

                return GetHashes(computedHashes);
            }
        }

        public override string HashFile(byte[] bytes)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var computedHashes = md5.ComputeHash(bytes);

                return GetHashes(computedHashes);
            }
        }
    }
}
