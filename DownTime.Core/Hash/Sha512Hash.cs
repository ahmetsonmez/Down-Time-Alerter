using System.Buffers.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DownTime.Core.Crypto;
using Base64 = DownTime.Core.Crypto.Base64;

namespace DownTime.Core.Hash
{
    public class Sha512Hash : IHash, ISaltHash
    {
        public override string HashText(string value)
        {
            using (var sha512 = new SHA512Managed())
            {
                var computeHashes = sha512.ComputeHash(Encoding.Default.GetBytes(value));
                var hashes = GetHashes(computeHashes);

                return hashes;
            }
        }

        public override string HashFile(string filePath)
        {
            using (var sha512 = new SHA512Managed())
            {
                var computeHashes = sha512.ComputeHash(File.ReadAllBytes(filePath));
                var hashes = GetHashes(computeHashes);

                return hashes;
            }
        }

        public override string HashFile(byte[] bytes)
        {
            using (var sha512 = new SHA512Managed())
            {
                var computeHashes = sha512.ComputeHash(bytes);
                var hashes = GetHashes(computeHashes);

                return hashes;
            }
        }

        public byte[] GetSalt(int maximumSaltLength = 16)
        {
            var salt = new byte[maximumSaltLength];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public string GetHash(string value)
        {
            try
            {
                var salt = GetSalt();

                var plainTextBytes = Encoding.UTF8.GetBytes(value);
                var plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (var i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (var i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                using (var sha512 = new SHA512Managed())
                {
                    var hashBytes = sha512.ComputeHash(plainTextWithSaltBytes);
                    var hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                    for (var i = 0; i < hashBytes.Length; i++)
                    {
                        hashWithSaltBytes[i] = hashBytes[i];
                    }

                    for (var i = 0; i < salt.Length; i++)
                    {
                        hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                    }

                    var base64 = new KeyProvider(new Base64());
                    return base64.Encrypt(hashWithSaltBytes);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetHash(string value, byte[] salt)
        {
            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(value);
                var plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (var i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (var i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                using (var sha512 = new SHA512Managed())
                {
                    var hashBytes = sha512.ComputeHash(plainTextWithSaltBytes);
                    var hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                    for (var i = 0; i < hashBytes.Length; i++)
                    {
                        hashWithSaltBytes[i] = hashBytes[i];
                    }

                    for (var i = 0; i < salt.Length; i++)
                    {
                        hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                    }

                    var base64 = new KeyProvider(new Base64());
                    return base64.Encrypt(hashWithSaltBytes);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool MatchHash(string value, string oldHashValue, byte[] salt)
        {
            var expectedHash = GetHash(value, salt);

            return oldHashValue == expectedHash;
        }
    }
}
