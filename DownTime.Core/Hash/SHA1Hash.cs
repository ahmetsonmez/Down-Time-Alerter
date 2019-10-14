using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DownTime.Core.Hash
{
    public class Sha1Hash : IHash
    {
        public override string HashText(string value)
        {
            using (var sha1 = SHA1.Create())
            {
                var computeHashes = sha1.ComputeHash(Encoding.Default.GetBytes(value));
                var hashes = GetHashes(computeHashes);

                return hashes;
            }
        }

        public override string HashFile(string filePath)
        {
            using (var sha1 = SHA1.Create())
            {
                var computeHashes = sha1.ComputeHash(File.ReadAllBytes(filePath));
                var hashes = GetHashes(computeHashes);

                return hashes;
            }
        }

        public override string HashFile(byte[] bytes)
        {
            using (var sha1 = SHA1.Create())
            {
                var computeHashes = sha1.ComputeHash(bytes);
                var hashes = GetHashes(computeHashes);

                return hashes;
            }
        }
    }
}
