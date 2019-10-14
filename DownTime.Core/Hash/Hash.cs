
namespace DownTime.Core.Hash
{
    public class Hash : IHash
    {
        private readonly IHash _hash;

        public Hash(IHash hash)
        {
            _hash = hash;
        }

        public override string HashText(string value)
        {
            return _hash.HashText(value);
        }

        public override string HashFile(string filePath)
        {
            return _hash.HashFile(filePath);
        }

        public override string HashFile(byte[] bytes)
        {
            return _hash.HashFile(bytes);
        }
    }
}
